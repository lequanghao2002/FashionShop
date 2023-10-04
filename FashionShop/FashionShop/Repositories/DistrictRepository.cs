
using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.ViewModel;
using System.Xml.Linq;

namespace FashionShop.Repositories
{
    public interface IDistrictRepository
    {
        public bool loadData();
    }
    public class DistrictRepository : IDistrictRepository
    {
        FashionShopDBContext _fashionShopDBContext;
        public DistrictRepository(FashionShopDBContext fashionShopDBContext) 
        {
            _fashionShopDBContext = fashionShopDBContext;
        }

        public bool loadData()
        {
            var xmlDocument = XDocument.Load("assets/customer/data/test.xml");
            var xmlElements = xmlDocument.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");


            foreach (var item in xmlElements)
            {
                var xmlElements2 = xmlDocument.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == int.Parse(item.Attribute("id").Value))
                .Elements("Item").Where(x => x.Attribute("type").Value == "district");

                foreach(var item2 in  xmlElements2)
                {
                    var xmlElements3 = xmlDocument.Element("Root")
                    .Elements("Item")
                    .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == int.Parse(item.Attribute("id").Value))
                    .Elements("Item")
                    .Single(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == int.Parse(item2.Attribute("id").Value))
                    .Elements("Item")
                    .Where(x => x.Attribute("type").Value == "ward");

                    foreach(var item3 in xmlElements3)
                    {
                        var check = _fashionShopDBContext.Wards.Count(p => p.ID == int.Parse(item3.Attribute("id").Value));
                        if (check == 0)
                        {
                            var Ward = new Ward()
                            {
                                ID = int.Parse(item3.Attribute("id").Value),
                                Name = item3.Attribute("value").Value,
                                ProvinceID = int.Parse(item.Attribute("id").Value),
                                DistrictID = int.Parse(item2.Attribute("id").Value),
                            };

                            _fashionShopDBContext.Add(Ward);
                            _fashionShopDBContext.SaveChanges();
                        }
                    }
                    
                }
            }

            return true;
        }
    }
}
