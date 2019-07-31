using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using E_Commerce_Site.Context;
using E_Commerce_Site.Models;
using PagedList;
using System.Web;
using System.IO;
using System.Web.Routing;

namespace E_Commerce_Site.Controllers
{
    public class ProductsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Products
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            //Image cache emptying
            TempData.Remove("ImageList");

            //Search

            var products = db.Products.AsQueryable();

            if (searchBy == "ProductId")
            {
                products = products.Where(x => x.ProductId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "ProductName")
            {
                products = products.Where(x => x.ProductName.StartsWith(search) || search == null);

            }
            else if (searchBy == "CategoryName")
            {
                products = products.Where(x => x.SubCategory.Category.CategoryName.StartsWith(search) || search == null);

            }
            else if (searchBy == "SubCategoryName")
            {
                products = products.Where(x => x.SubCategory.SubCategoryName.StartsWith(search) || search == null);

            }



            //sort

            ViewBag.SortProductIdParameter = sortBy == "ProductId" ? "ProductId Desc" : "ProductId";
            ViewBag.SortProductNameParameter = sortBy == "ProductName" ? "ProductName Desc" : "ProductName";
            ViewBag.SortProductDescriptionParameter = sortBy == "ProductDescription" ? "ProductDescription Desc" : "ProductDescription";
            ViewBag.SortCategoryNameParameter = sortBy == "CategoryName" ? "CategoryName Desc" : "CategoryName";
            ViewBag.SortSubCategoryNameParameter = sortBy == "SubCategoryName" ? "SubCategoryName Desc" : "SubCategoryName";
            ViewBag.SortQuantityParameter = sortBy == "Quantity" ? "Quantity Desc" : "Quantity";
            ViewBag.SortCadPriceParameter = sortBy == "CadPrice" ? "CadPrice Desc" : "CadPrice";
            ViewBag.SortInrPriceParameter = sortBy == "InrPrice" ? "InrPrice Desc" : "InrPrice";
            ViewBag.SortColorsParameter = sortBy == "Colors" ? "Colors Desc" : "Colors";
            ViewBag.SortShippingCostParameter = sortBy == "ShippingCost" ? "ShippingCost Desc" : "ShippingCost";
            ViewBag.SortSizeParameter = sortBy == "Sizes" ? "Sizes Desc" : "Sizes";
            ViewBag.SortDiscountParameter = sortBy == "Discount" ? "Discount Desc" : "Discount";
            ViewBag.SortOnHomePageParameter = sortBy == "OnHomePage" ? "OnHomePage Desc" : "OnHomePage";
            ViewBag.SortRatingParameter = sortBy == "Rating" ? "Rating Desc" : "Rating";





            switch (sortBy)
            {
                case "ProductId":
                    products = products.OrderBy(x => x.ProductId);
                    break;
                case "ProductId Desc":
                    products = products.OrderByDescending(x => x.ProductId);
                    break;
                case "ProductName":
                    products = products.OrderBy(x => x.ProductName);
                    break;
                case "ProductName Desc":
                    products = products.OrderByDescending(x => x.ProductName);
                    break;
                case "ProductDescription":
                    products = products.OrderBy(x => x.ProductDescription);
                    break;
                case "ProductDescription Desc":
                    products = products.OrderByDescending(x => x.ProductDescription);
                    break;
                case "CadPrice":
                    products = products.OrderBy(x => x.PriceCad);
                    break;
                case "CadPrice Desc":
                    products = products.OrderByDescending(x => x.PriceCad);
                    break;
                case "InrPrice":
                    products = products.OrderBy(x => x.PriceInr);
                    break;
                case "InrPrice Desc":
                    products = products.OrderByDescending(x => x.PriceInr);
                    break;
                case "CategoryName":
                    products = products.OrderBy(x => x.SubCategory.Category.CategoryName);
                    break;
                case "CategoryName Desc":
                    products = products.OrderByDescending(x => x.SubCategory.Category.CategoryName);
                    break;
                case "SubCategoryName":
                    products = products.OrderBy(x => x.SubCategory.SubCategoryName);
                    break;
                case "SubCategoryName Desc":
                    products = products.OrderByDescending(x => x.SubCategory.SubCategoryName);
                    break;
                case "Quantity":
                    products = products.OrderBy(x => x.Quantity);
                    break;
                case "Quantity Desc":
                    products = products.OrderByDescending(x => x.Quantity);
                    break;
                case "Discount":
                    products = products.OrderBy(x => x.Discount);
                    break;
                case "Discount Desc":
                    products = products.OrderByDescending(x => x.Discount);
                    break;
                case "ShippingCost":
                    products = products.OrderBy(x => x.ShippingCost);
                    break;
                case "ShippingCost Desc":
                    products = products.OrderByDescending(x => x.ShippingCost);
                    break;
                case "OnHomePage":
                    products = products.OrderBy(x => x.IsVisibleOnHome);
                    break;
                case "OnHomePage Desc":
                    products = products.OrderByDescending(x => x.IsVisibleOnHome);
                    break;
                case "Rating":
                    products = products.OrderBy(x => x.TotalRating);
                    break;
                case "Rating Desc":
                    products = products.OrderByDescending(x => x.TotalRating);
                    break;
                default:
                    products = products.OrderByDescending(x => x.ProductId);
                    break;
            }

           //products.Include(p => p.Image).Include(p => p.SubCategory);
            return View(products.ToPagedList(page ?? 1, 50));


        }



        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
     
        
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            //colorid to colorname transformation logic
            List<Image> imageList = product.ImageList.Where(x => x.ProductId == id).ToList();
            List<Color> ColorList = db.Colors.ToList();
            Dictionary<int, String> ColorNamesDict = new Dictionary<int, String>();

            for (int i = 0; i < imageList.Count; i++)
            {
                foreach ( Color item in ColorList)
                {
                    if (imageList[i].ColorId == item.ColorId)
                    {
                        if (!ColorNamesDict.ContainsKey((imageList[i].ColorId))) { 
                   
                            ColorNamesDict.Add(imageList[i].ColorId, item.ColorName);
                        }
                    }
                }
                
            }

      
            //passing the dictionary
            ViewBag.ColorNames = ColorNamesDict;



            return View(product);
        }


        // GET: Products/AddImageEdit
        [HttpGet]
        public ActionResult AddImageEdit(Product product)
        {

            // ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");


            return View(product);




        }
        // POST: Products/AddImageEdit
        [HttpPost]
        public ActionResult AddImageEdit(FormCollection form, HttpPostedFileBase ImageFile, int? id)
        {

            string ColorListSelect = form["ColorListSelect"];
            string ImagePositionsListSelect = form["ImagePositionsListSelect"];

            Product product = db.Products.Single(x => x.ProductId == id);

            Image imgTemp = new Image()
            {

                ImageAngle = ImagePositionsListSelect,
                ColorId = int.Parse(ColorListSelect)

            };

            if (product.ImageList == null)
            {
                product.ImageList = new List<Image>();
            }



            //check to see if the image already exists,no duplicates

            if (imgTemp.ImageAngle != null && IsImageUnique(imgTemp, product))
            {
                product.ImageList.Add(imgTemp);

            }


            //save to server
            if (ImageFile != null)
            {
                ImageFile.SaveAs(HttpContext.Server.MapPath("/Images/" + ImageFile.FileName));


                imgTemp.Address = "/Images/" + ImageFile.FileName;

            }

            //return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "Products", action = "Edit", Id = id,img = img }));
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = id });

        }




        // POST: Products/DeleteImageEdit
        public ActionResult DeleteImageEdit(int? productId, int? imgId)
        {
            //if the last image is tried to be deleted the product parameter doesnt come.So the check auto redirects.
            //if (product == null)
            //{ return RedirectToAction("Edit", product); }

            //reload i check et
            Image img = db.Images.Single(x => x.ImageId == imgId);
            Product product = db.Products.Single(x => x.ProductId == productId);


            product.ImageList.Remove(img);
            product.ImageList.RemoveAll(x => x.Address == img.Address);
            db.Images.Remove(db.Images.Single(x => x.ImageId == img.ImageId));

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();


            //Delete Image from Server
            string fullPath = Request.MapPath("~\\" + img.Address);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }



            return RedirectToAction("Edit", new { id = product.ProductId });

        }

        // GET: Products/AddImage
        [HttpGet]
        public ActionResult AddImage(Product product)
        {

            product.ImageList = TempData["ImageList"] as List<Image>;
            TempData.Keep();




            // ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");


            return View("AddImage", product);




        }
        // POST: Products/AddImage
        [HttpPost]
        public ActionResult AddImage(FormCollection form, HttpPostedFileBase ImageFile)
        {

            //check to see if it is called from edit or not one gore redirect
            //viewbag le yapicaksan dikkat et refresh yapinca viewbag bosalior,bos olunca eyw dersin bu sefer create e gider.aynisi digeri icinde gecerli
            //TempData kullanabilirsin onu check et,bu olurlu gibi
            //eger ayri controller/view yapacaksan eyw




            string ColorListSelect = form["ColorListSelect"];
            string ImagePositionsListSelect = form["ImagePositionsListSelect"];

       
            Image img = new Image()
            {

                ImageAngle = ImagePositionsListSelect,
                ColorId = int.Parse(ColorListSelect)

            };

            if (ImageFile != null)
            {
                ImageFile.SaveAs(HttpContext.Server.MapPath("/Images/" + ImageFile.FileName));


                img.Address = "/Images/" + ImageFile.FileName;

            }



            return RedirectToAction("Create", img);

        }

        // POST: Products/DeleteImage
        public ActionResult DeleteImage(Product product, Image img, HttpPostedFileBase ImageFile)
        {
            //if the last image is tried to be deleted the product parameter doesnt come.So the check auto redirects.
            //if (product == null)
            //{ return RedirectToAction("Edit", product); }


            Image imgtemp = img;
            product.ImageList = TempData["ImageList"] as List<Image>;
            product.ImageList.Remove(imgtemp);
            product.ImageList.RemoveAll(x => x.Address == img.Address);
            TempData["ImageList"] = product.ImageList;

            TempData.Keep();



            //Delete Image from Server
            string fullPath = Request.MapPath("~\\" + img.Address);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }



            return RedirectToAction("Create", product);

        }


        // GET: Products/Create
        public ActionResult Create(Product product, Image img)
        {
            product.ImageList = TempData["ImageList"] as List<Image>;
            TempData.Keep();

            product.TagList = db.Tags.ToList();


            if (product.ImageList == null)
            {
                product.ImageList = new List<Image>();
            }


            //check to see if the image already exists,no duplicates
            if (img.ImageAngle != null && IsImageUnique(img, product))
            {
                product.ImageList.Add(img);

            }

            if (product.ImageList.Count() >= 1)
            {
                TempData["ImageList"] = product.ImageList;
                TempData.Keep();
            }

            //colorid to colorname transformation logic
            List<Image> imageList = product.ImageList.Where(x => x.ProductId == product.ProductId).ToList();
            List<Color> ColorList = db.Colors.ToList();
            Dictionary<int, String> ColorNamesDict = new Dictionary<int, String>();

            for (int i = 0; i < imageList.Count; i++)
            {
                foreach (Color item in ColorList)
                {
                    if (imageList[i].ColorId == item.ColorId)
                    {
                        if (!ColorNamesDict.ContainsKey((imageList[i].ColorId)))
                        {

                            ColorNamesDict.Add(imageList[i].ColorId, item.ColorName);
                        }
                    }
                }

            }


            //passing the dictionary
            ViewBag.ColorNames = ColorNamesDict;




            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);
            ModelState.Clear();
            return View(product);
        }




        // POST: Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, FormCollection form)
        {

            product.TagList = new List<Tag>();
            product.ImageList = TempData["ImageList"] as List<Image>;


            if (product.ImageList == null)
            {
                ViewBag.Message = "You have to Upload at least one Image";
                return RedirectToAction("Create", product);


            }
            TempData.Clear();


            if (ModelState.IsValid)
            {
                //Taglist logic
                string Taglist = form["TagListSelect"];
                if (Taglist != null)
                {
                    string[] wordsString = Taglist.Split(',');
                    int[] wordsInt = Array.ConvertAll(wordsString, int.Parse);
                    foreach (int value in wordsInt)
                    {
                        product.TagList.Add(db.Tags.Single(m => m.TagId.Equals(value)));
                    }
                }


                product.Created = DateTimeOffset.Now;
                product.Updated = DateTimeOffset.Now;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

            }



            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id, Product product)
        {


            product = db.Products.Where(x => x.ProductId == id).Include(x => x.ImageList).First();


            //colorid to colorname transformation logic
            List<Image> imageList = product.ImageList.Where(x => x.ProductId == product.ProductId).ToList();
            List<Color> ColorList = db.Colors.ToList();
            Dictionary<int, String> ColorNamesDict = new Dictionary<int, String>();

            for (int i = 0; i < imageList.Count; i++)
            {
                foreach (Color item in ColorList)
                {
                    if (imageList[i].ColorId == item.ColorId)
                    {
                        if (!ColorNamesDict.ContainsKey((imageList[i].ColorId)))
                        {

                            ColorNamesDict.Add(imageList[i].ColorId, item.ColorName);
                        }
                    }
                }

            }


            //passing the dictionary
            ViewBag.ColorNames = ColorNamesDict;















            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "TagName", product.TagList);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, FormCollection form)
        {
            //[Bind(Include = "ProductId,ProductName,Price,ProductDescription,Quantity,Discount,SubCategoryId,ShippingCost,IsVisibleOnHome")]
            //,Color,Size,Reviews
            product.TagList = new List<Tag>();

            if (ModelState.IsValid)
            {
                product.Updated = DateTimeOffset.Now;

                string Taglist = form["TagListSelect"];
                if (Taglist != null)
                {
                    string[] wordsString = Taglist.Split(',');
                    int[] wordsInt = Array.ConvertAll(wordsString, int.Parse);
                    foreach (int value in wordsInt)
                    {
                        product.TagList.Add(db.Tags.Single(m => m.TagId.Equals(value)));
                    }
                }

                db.Entry(product).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", product.SubCategoryId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "TagName", db.Tags);


            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            Product product = db.Products.Find(id);
            IEnumerable<Inventory> inventories = db.Inventories.Where(x => x.ProductId == id).ToList();

            if (inventories.Count() != 0)
            {
                return RedirectToAction("Index");


            }


            //delete images also
            Image image = db.Images.Find(id);


            db.Products.Remove(product);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public bool IsImageUnique(Image img, Product product)
        {


            for (int i = 0; i < product.ImageList.Count(); i++)
            {
                var x = product.ImageList.ElementAt(i).ColorId;
                var z = product.ImageList.ElementAt(i).ImageAngle;
                var y = product.ImageList.ElementAt(i).Address;

                if ((x == img.ColorId && z == img.ImageAngle) || y == img.Address)
                {
                    return false;
                }
            }

            return true;
        }





    }
}

