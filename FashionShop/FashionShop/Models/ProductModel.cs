using FashionShop.Data;
using FashionShop.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sitecore.FakeDb;
using System;
using System.Linq;
using System.Web;


namespace FashionShop.Models
{
    public class ProductModel
    {
        public static List<Product> list_product(int id )
        {
            FashionShopDBContext db = new FashionShopDBContext();
            var query = from pro in db.Products where pro.ID == id
                        select pro;
            return query.ToList<Product>();  
        }
    }
}
