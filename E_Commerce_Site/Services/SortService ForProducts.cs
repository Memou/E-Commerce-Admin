//using E_Commerce_Site.Context;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using PagedList;

//namespace E_Commerce_Site.Services
//{
//    public static class SortService_ForProducts
//    {
//        private static MainContext db = new MainContext();

//        public static IQueryable<E_Commerce_Site.Models.Product> SortProducts(string sortBy,string parameter) {
//            var products = db.Products.AsQueryable();

//            var SortProductIdParameter = sortBy == "ProductId" ? "ProductId Desc" : "ProductId";
//            var SortProductNameParameter = sortBy == "ProductName" ? "ProductName Desc" : "ProductName";
//            var SortProductDescriptionParameter = sortBy == "ProductDescription" ? "ProductDescription Desc" : "ProductDescription";
//            var SortCategoryNameParameter = sortBy == "CategoryName" ? "CategoryName Desc" : "CategoryName";
//            var SortSubCategoryNameParameter = sortBy == "SubCategoryName" ? "SubCategoryName Desc" : "SubCategoryName";
//            var SortQuantityParameter = sortBy == "Quantity" ? "Quantity Desc" : "Quantity";
//            var SortCadPriceParameter = sortBy == "CadPrice" ? "CadPrice Desc" : "CadPrice";
//            var SortInrPriceParameter = sortBy == "InrPrice" ? "InrPrice Desc" : "InrPrice";
//            var SortColorsParameter = sortBy == "Colors" ? "Colors Desc" : "Colors";
//            var SortShippingCostParameter = sortBy == "ShippingCost" ? "ShippingCost Desc" : "ShippingCost";
//            var SortSizeParameter = sortBy == "Sizes" ? "Sizes Desc" : "Sizes";
//            var SortDiscountParameter = sortBy == "Discount" ? "Discount Desc" : "Discount";
//            var SortOnHomePageParameter = sortBy == "OnHomePage" ? "OnHomePage Desc" : "OnHomePage";
//            var SortRatingParameter = sortBy == "Rating" ? "Rating Desc" : "Rating";



//            switch (sortBy)
//            {
//                case "ProductId":
//                    products = products.OrderBy(x => x.ProductId);
//                    break;
//                case "ProductId Desc":
//                    products = products.OrderByDescending(x => x.ProductId);
//                    break;
//                case "ProductName":
//                    products = products.OrderBy(x => x.ProductName);
//                    break;
//                case "ProductName Desc":
//                    products = products.OrderByDescending(x => x.ProductName);
//                    break;
//                case "ProductDescription":
//                    products = products.OrderBy(x => x.ProductDescription);
//                    break;
//                case "ProductDescription Desc":
//                    products = products.OrderByDescending(x => x.ProductDescription);
//                    break;
//                case "CadPrice":
//                    products = products.OrderBy(x => x.PriceCad);
//                    break;
//                case "CadPrice Desc":
//                    products = products.OrderByDescending(x => x.PriceCad);
//                    break;
//                case "InrPrice":
//                    products = products.OrderBy(x => x.PriceInr);
//                    break;
//                case "InrPrice Desc":
//                    products = products.OrderByDescending(x => x.PriceInr);
//                    break;
//                case "CategoryName":
//                    products = products.OrderBy(x => x.SubCategory.Category.CategoryName);
//                    break;
//                case "CategoryName Desc":
//                    products = products.OrderByDescending(x => x.SubCategory.Category.CategoryName);
//                    break;
//                case "SubCategoryName":
//                    products = products.OrderBy(x => x.SubCategory.SubCategoryName);
//                    break;
//                case "SubCategoryName Desc":
//                    products = products.OrderByDescending(x => x.SubCategory.SubCategoryName);
//                    break;
//                case "Quantity":
//                    products = products.OrderBy(x => x.Quantity);
//                    break;
//                case "Quantity Desc":
//                    products = products.OrderByDescending(x => x.Quantity);
//                    break;
//                case "Discount":
//                    products = products.OrderBy(x => x.Discount);
//                    break;
//                case "Discount Desc":
//                    products = products.OrderByDescending(x => x.Discount);
//                    break;
//                case "ShippingCost":
//                    products = products.OrderBy(x => x.ShippingCost);
//                    break;
//                case "ShippingCost Desc":
//                    products = products.OrderByDescending(x => x.ShippingCost);
//                    break;
//                case "OnHomePage":
//                    products = products.OrderBy(x => x.IsVisibleOnHome);
//                    break;
//                case "OnHomePage Desc":
//                    products = products.OrderByDescending(x => x.IsVisibleOnHome);
//                    break;
//                case "Rating":
//                    products = products.OrderBy(x => x.TotalRating);
//                    break;
//                case "Rating Desc":
//                    products = products.OrderByDescending(x => x.TotalRating);
//                    break;
//                default:
//                    products = products.OrderByDescending(x => x.ProductId);
//                    break;
//            }
//            return products;
//        } }

//    }
