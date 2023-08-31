using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO.UserDTO;
using FashionShop.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FashionShop.Models;
using Microsoft.Owin.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Web;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace FashionShop.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<User> userManager, IUserRepository userRepository )
        {
            this._userManager = userManager;
            _userRepository = userRepository;
        }

        public ActionResult Login( string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
           if(ModelState.IsValid)
            {
                var checkUser = await _userManager.FindByEmailAsync(loginRequestDTO.Email);
                if (checkUser == null)
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại");
                    return View();
                }
                if (checkUser != null)
                {
                    var checkPassword = await _userManager.CheckPasswordAsync(checkUser, loginRequestDTO.Password);
                    if (checkPassword)
                    {
                        if (checkUser.LockoutEnabled == false)
                        {
                            HttpContext.Session.SetString("UserName", checkUser.Email);
                            ViewData["mssLogin"] = "Đăng nhập thành công!";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewData["ErrorLogin"] = "tài khoản của bạn hiện đang bị khóa tạm thời.";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hãy kiểm tra lại mật khẩu hoặc Email!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Hãy điền đầy đủ thông tin email và mật khẩu!!");
                }
            }
           //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
        
 

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterAdminRequestDTO registerAdminRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var customerByEmail = await _userManager.FindByEmailAsync(registerAdminRequestDTO.Email);
                if (customerByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email này đã tồn tại");
                    return View();
                }
                
                if (registerAdminRequestDTO.Password != registerAdminRequestDTO.RePassword)
                {
                    ModelState.AddModelError("Password", "Mat khau khong giong nhau");
                    return View();
                }

                var CustomerRegister = await _userRepository.RegisterAccountCustomer(registerAdminRequestDTO);

                if(CustomerRegister == true)
                {
                    ViewData["SuccessMsg"] = " Đăng ký thành công";
                }
                else
                {

                }
                //var CustomerRegister = new User
                //{
                //    FullName = registerRequestDTO.FullName,
                //    UserName = registerRequestDTO.Email,
                //    Email = registerRequestDTO.Email,
                //    PhoneNumber = registerRequestDTO.PhoneNumber,
                //};

                //var result = await _userManager.CreateAsync(CustomerRegister, registerRequestDTO.Password);

                //if (result.Succeeded)
                //{
                //    await _userManager.AddToRoleAsync(CustomerRegister, "khách hàng");
                //}
                //ViewData["SuccessMsg"] = " Đăng ký thành công";


            }
            return View();
        }
    }
}
