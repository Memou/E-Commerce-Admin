using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce_Site.Context;
using E_Commerce_Site.Models;
using PagedList;

namespace E_Commerce_Site.Controllers
{
    public class SubCategoriesController : Controller
    {
        private MainContext db = new MainContext();

        // GET: SubCategories
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            var subcategories = db.SubCategories.AsQueryable();

            if (searchBy == "SubCategoryId")
            {
                subcategories = subcategories.Where(x => x.SubCategoryId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "SubCategoryName")
            {
                subcategories = subcategories.Where(x => x.SubCategoryName.StartsWith(search) || search == null);

            }
            else if (searchBy == "CategoryName")
            {
                subcategories = subcategories.Where(x => x.Category.CategoryName.StartsWith(search) || search == null);

            }

            ViewBag.SortSubCategoryIdParameter = sortBy == "SubCategoryId" ? "SubCategoryId Desc" : "SubCategoryId";
            ViewBag.SortSubCategoryNameParameter = sortBy == "SubCategoryName" ? "SubCategoryName Desc" : "SubCategoryName";
            ViewBag.SortCategoryNameParameter = sortBy == "CategoryName" ? "CategoryName Desc" : "CategoryName";

            switch (sortBy)
            {
                case "SubCategoryId":
                    subcategories = subcategories.OrderBy(x => x.SubCategoryId);
                    break;
                case "SubCategoryId Desc":
                    subcategories = subcategories.OrderByDescending(x => x.SubCategoryId);
                    break;

                case "SubCategoryName":
                    subcategories = subcategories.OrderBy(x => x.SubCategoryName);
                    break;

                case "SubCategoryName Desc":
                    subcategories = subcategories.OrderByDescending(x => x.SubCategoryName);
                    break;
                case "CategoryName":
                    subcategories = subcategories.OrderBy(x => x.Category.CategoryName);
                    break;

                case "CategoryName Desc":
                    subcategories = subcategories.OrderByDescending(x => x.Category.CategoryName);
                    break;


                default:
                    subcategories = subcategories.OrderBy(x => x.SubCategoryName);
                    break;
            }

            return View(subcategories.ToPagedList(page ?? 1, 100));

        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ModelState.Clear();
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryId,SubCategoryName,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryId,SubCategoryName,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            SubCategory subCategory = db.SubCategories.Find(id);
            IEnumerable<Product> products = db.Products.Where(x => x.SubCategoryId == id).ToList();

            if (products.Count() != 0) {
                return RedirectToAction("Index");


            }
            

            //Category switching logic
            //{
            //    if(db.Categories.Any(x => x.CategoryName == "Unassigned"))
            //    db.Categories.Add(new Category("Unassigned"));
            //    db.SaveChanges();

            //    Category catUnassigned = db.Categories.Single(x => x.CategoryName == "Unassigned");

            //    db.SubCategories.Add(new SubCategory("Unassigned", catUnassigned.CategoryId));
            //    db.SaveChanges();

            //    SubCategory subcatUnassigned = db.SubCategories.Single(x => x.SubCategoryName == "Unassigned");

            //    foreach (var item in products)
            //    {
            //        item.SubCategoryId = subcatUnassigned.SubCategoryId;
            //    }
            //    db.SaveChanges();
            //}



            //Product removal logic
            // List<Image> images = new List<Image>();
            //Image image = null;
            //foreach (var item in products)
            //{
            //    images.Add(db.Images.Single(x => x.ProductId == item.ProductId));


            //}
            //db.Images.RemoveRange(images);



            db.SubCategories.Remove(subCategory);
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
    }
}
