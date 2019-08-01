//using E_Commerce_Site.Context;
//using E_Commerce_Site.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace E_Commerce_Site.Services
//{
//    public static class SearchServiceForProducts
//    {

//        //This service can be made generic for all models,right now it only works for get/products

//        private static MainContext db = new MainContext();

//        public static IQueryable<E_Commerce_Site.Models.Product> searchforProduct(string searchBy,string search) {
//            var products = db.Products.AsQueryable();

//            if (searchBy == "ProductId")
//            {
//                products = products.Where(x => x.ProductId.ToString().StartsWith(search) || search == null);

//            }
//            else if (searchBy == "ProductName")
//            {
//                products = products.Where(x => x.ProductName.StartsWith(search) || search == null);

//            }
//            else if (searchBy == "CategoryName")
//            {
//                products = products.Where(x => x.SubCategory.Category.CategoryName.StartsWith(search) || search == null);

//            }
//            else if (searchBy == "SubCategoryName")
//            {
//                products = products.Where(x => x.SubCategory.SubCategoryName.StartsWith(search) || search == null);

//            }

//            return products;
//        }

//    }
//}