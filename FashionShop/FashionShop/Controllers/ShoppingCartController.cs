using Blogger_Common;
using FashionShop.Helper;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO.OrderDTO;
using FashionShop.Models.DTO.UserDTO;
using FashionShop.Models.ViewModel;
using FashionShop.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;
using System.Xml.Linq;

namespace FashionShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IOrderRepository _orderRepository;

        public ShoppingCartController(IProductRepository productRepository, IVoucherRepository voucherRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);

            if (cartSession == null)
            {
                var emptyCart = new List<ShoppingCartViewModel>();

                HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(emptyCart));

                cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);
            };

            return View();
        }

        public JsonResult GetAll()
        {
            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);

            if (cartSession == null)
            {
                var emptyCart = new List<ShoppingCartViewModel>();

                HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(emptyCart));

                cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);
            };

            var cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);

            return Json(new
            {
                data = cart,
                status = true,
            });
        }

        [HttpPost]
        public JsonResult Add(int productID)
        {
            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);

            if (cartSession == null)
            {
                var emptyCart = new List<ShoppingCartViewModel>();

                HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(emptyCart));
            };

            var cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);

            if (cart.Any(c => c.ProductID == productID))
            {
                foreach (var item in cart)
                {
                    if (item.ProductID == productID)
                    {
                        item.Quantity++;
                    }
                }
            }
            else
            {
                var newItem = new ShoppingCartViewModel();
                newItem.ProductID = productID;
                newItem.Quantity = 1;
                newItem.Product = _productRepository.GetId(productID);

                cart.Add(newItem);
            }

            HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(cart));

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartData);

            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);

            var cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);

            foreach (var item in cart)
            {
                foreach (var item2 in cartViewModel)
                {
                    if (item.ProductID == item2.ProductID)
                    {
                        item.Quantity = item2.Quantity;
                    };
                };
            };

            HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(cart));


            return Json(new
            {
                status = true,
            });
        }


        [HttpPost]
        public JsonResult Delete(int productID)
        {
            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);

            if (cartSession != null)
            {
                var cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);

                cart.RemoveAll(c => c.ProductID == productID);

                HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(cart));

                return Json(new
                {
                    status = true,
                });
            };

            return Json(new
            {
                status = false,
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            var emptyCart = new List<ShoppingCartViewModel>();

            HttpContext.Session.SetString(CommonConstants.SessionCart, JsonConvert.SerializeObject(emptyCart));

            return Json(new
            {
                status = true,
            });
        }

        public IActionResult CheckOut()
        {
            string userSession = HttpContext.Session.GetString(CommonConstants.SessionUser);

            if (userSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userSession);

                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CreateOrderDTO createOrderDTO)
        {
            if(string.IsNullOrEmpty(createOrderDTO.FullName))
            {
                ViewBag.errorFullName = "Vui lòng nhập tên";
            }

            if (string.IsNullOrEmpty(createOrderDTO.PhoneNumber))
            {
                ViewBag.errorPhoneNumber = "Vui lòng nhập số điện thoại";
            }

            if (string.IsNullOrEmpty(createOrderDTO.Address))
            {
                ViewBag.errorAddress = "Vui lòng nhập địa chỉ giao hàng";
            }
            
            if (createOrderDTO.ProvinceID == 0)
            {
                ViewBag.errorProvinceID = "Vui lòng chọn Tỉnh / Thành";
            }
            
            if (createOrderDTO.DistrictID == 0)
            {
                ViewBag.errorDistrictID = "Vui lòng chọn Quận / Huyện";
            }
            
            if (createOrderDTO.WardID == 0)
            {
                ViewBag.errorWardID = "Vui lòng chọn Phường / Xã";
            }

            if(ModelState.IsValid)
            {
                string cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);
                var cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);

                createOrderDTO.shoppingCarts = cart;

                var order = await _orderRepository.Create(createOrderDTO);

                if (order != null)
                {
                    return RedirectToAction("OrderSuccess");
                }
            }

            // Tự gán lại giá trị khi nhập sai
            string userSession = HttpContext.Session.GetString(CommonConstants.SessionUser);
            var user = JsonConvert.DeserializeObject<User>(userSession);

            return View();
        }

        public async Task<IActionResult> OrderSuccess()
        {
            HttpContext.Session.Remove(CommonConstants.SessionCart);

            var sessionUser = HttpContext.Session.GetString(CommonConstants.SessionUser);
            var user = JsonConvert.DeserializeObject<User>(sessionUser);

            var order = await _orderRepository.GetNewByUserID(user.Id);

            // Gửi đơn hàng đến email của khách hàng khi vừa đặt hàng thành công
            var html = "";
            double totalMoney = 0;

            foreach (var item in order.OrderDetails)
            {
                html += "<tr>";

                var productName = item.Product.Name;
                var quantity = item.Quantity;
                if (item.Product.Discount > 0)
                {
                    var price = item.Product.Price - (item.Product.Price * item.Product.Discount / 100);
                    totalMoney += price * quantity;

                    html += "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + productName + "</td>"
                    + "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + price.ToString("C0", new CultureInfo("vi-VN")) + "</td>\r\n"
                    + "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + quantity + "</td>\r\n"
                    + "<td style=\"border: 1px solid black; padding: 8px; text-align: right;\">" + (price * quantity).ToString("C0", new CultureInfo("vi-VN")) + "</td>";
                }
                else
                {
                    var price = item.Product.Price;
                    totalMoney += price * quantity;

                    html += "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + productName + "</td>"
                   + "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + price.ToString("C0", new CultureInfo("vi-VN")) + "</td>\r\n"
                   + "<td style=\"border: 1px solid black; padding: 8px; text-align: center;\">" + quantity + "</td>\r\n"
                   + "<td style=\"border: 1px solid black; padding: 8px; text-align: right;\">" + (price * quantity).ToString("C0", new CultureInfo("vi-VN")) + "</td>";
                }

                html += "</tr>";
            }

            double totalPayment = totalMoney + order.DeliveryFee;

            string template = System.IO.File.ReadAllText("assets/customer/template/order.html");

            template = template.Replace("{{orderShopping}}", html);
            template = template.Replace("{{totalMoney}}", totalMoney.ToString("C0", new CultureInfo("vi-VN")));
            template = template.Replace("{{deliveryFee}}", order.DeliveryFee.ToString("C0", new CultureInfo("vi-VN")));
            double voucherValue = 0;
            if (order.Voucher != null)
            {
                if (order.Voucher.DiscountAmount == true)
                {
                    voucherValue = order.Voucher.DiscountValue;
                }
                else
                {
                    voucherValue = totalMoney * order.Voucher.DiscountValue / 100;
                }

                template = template.Replace("{{voucher}}", voucherValue.ToString("C0", new CultureInfo("vi-VN")));
                totalPayment -= voucherValue;
            }
            else
            {
                template = template.Replace("{{voucher}}", "0");
            }
            template = template.Replace("{{totalPayment}}", totalPayment.ToString("C0", new CultureInfo("vi-VN")));

            template = template.Replace("{{fullName}}", order.FullName);
            template = template.Replace("{{email}}", order.Email);
            template = template.Replace("{{phoneNumber}}", order.PhoneNumber);

            template = template.Replace("{{province}}", order.ProvinceID.ToString());
            template = template.Replace("{{district}}", order.DistrictID.ToString());
            template = template.Replace("{{ward}}", order.WardID.ToString());
            template = template.Replace("{{address}}", order.Address);
            template = template.Replace("{{note}}", order.Note);

            EmailService.SendMail("FashionShop", "Đặt hàng thành công", template, user.Email);

            return View(order);
        }

        public JsonResult LoadProvince()
        {
            var xmlDocument = XDocument.Load("assets/customer/data/Provinces_Data.xml");

            var xmlElements = xmlDocument.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");

            var listProvinces = new List<ProvinceViewModel>();
            ProvinceViewModel provinceVM = null;

            foreach (var item in xmlElements)
            {
                provinceVM = new ProvinceViewModel()
                {
                    ID = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value
                };

                listProvinces.Add(provinceVM);
            }

            return Json(new
            {
                data = listProvinces.OrderBy(p => p.Name),
                status = true,
            });

        }

        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDocument = XDocument.Load("assets/customer/data/Provinces_Data.xml");

            var xmlElements = xmlDocument.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID)
                .Elements("Item").Where(x => x.Attribute("type").Value == "district");

            var listDistricts = new List<DistrictViewModel>();
            DistrictViewModel districtVM = null;

            foreach (var item in xmlElements)
            {
                districtVM = new DistrictViewModel()
                {
                    ID = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value,
                    ProvinceID = provinceID
                };

                listDistricts.Add(districtVM);
            }

            return Json(new
            {
                data = listDistricts.OrderBy(p => p.Name),
                status = true,
            });

        }

        public JsonResult LoadWard(int provinceID, int districtID)
        {
            var xmlDocument = XDocument.Load("assets/customer/data/Provinces_Data.xml");

            var xmlElements = xmlDocument.Element("Root")
                .Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID)
                .Elements("Item")
                .Single(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == districtID)
                .Elements("Item")
                .Where(x => x.Attribute("type").Value == "ward");

            var listWards = new List<WardViewModel>();
            WardViewModel wardVM = null;

            foreach (var item in xmlElements)
            {
                wardVM = new WardViewModel()
                {
                    ID = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value,
                    ProvinceID = provinceID,
                    DistrictID = districtID,
                };

                listWards.Add(wardVM);
            }

            return Json(new
            {
                data = listWards.OrderBy(p => p.Name),
                status = true,
            });

        }

        public JsonResult LoadTotalMoneyOfCart()
        {
            var cartSession = HttpContext.Session.GetString(CommonConstants.SessionCart);
            var cart = new List<ShoppingCartViewModel>();

            if (cartSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<ShoppingCartViewModel>>(cartSession);
            }

            double totalMoney = 0;
            foreach (var item in cart)
            {
                if (item.Product.Discount > 0)
                {
                    var price = item.Product.Price - (item.Product.Price * item.Product.Discount / 100);
                    totalMoney += price * item.Quantity;
                }
                else
                {
                    totalMoney += item.Product.Price * item.Quantity;
                }
            }

            return Json(new
            {
                data = totalMoney,
                status = true,
            });

        }

        public async Task<JsonResult> CheckVoucher(string discountCode)
        {
            var voucher = await _voucherRepository.GetByDiscountCode(discountCode);

            if (voucher != null)
            {
                return Json(new
                {
                    data = voucher,
                    status = true,
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                });
            }
        }
    }
}
