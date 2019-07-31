using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using E_Commerce_Site.Context;
using E_Commerce_Site.Models;

namespace E_Commerce_Site.Controllers
{
    public class CategoriesController : Controller
    {
        private MainContext db = new MainContext();
        

        // GET: Categories
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            var category = db.Categories.AsQueryable();

            if (searchBy == "CategoryId")
            {
                category = category.Where(x => x.CategoryId.ToString().StartsWith(search)|| search == null);

            }
            else if (searchBy == "CategoryName")
            {
                category = category.Where(x => x.CategoryName.StartsWith(search) || search == null);

            }


            ViewBag.SortCategoryIdParameter = sortBy == "CategoryId" ? "CategoryId Desc" : "CategoryId";
            ViewBag.SortCategoryNameParameter = sortBy == "CategoryName" ? "CategoryName Desc" : "CategoryName";

            switch (sortBy)
            {
                case "CategoryId":
                    category = category.OrderBy(x => x.CategoryId);
                    break;
                case "CategoryId Desc":
                    category = category.OrderByDescending(x => x.CategoryId);
                    break;

                case "CategoryName":
                    category = category.OrderBy(x => x.CategoryName);
                    break;

                case "CategoryName Desc":
                    category = category.OrderByDescending(x => x.CategoryName);
                    break;
              
                default:
                    category = category.OrderBy(x => x.CategoryName);
                    break;
            }

            return View(category.ToPagedList(page ?? 1, 100));

        }


        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);

            IEnumerable<SubCategory> subCategories = db.SubCategories.Where(x => x.CategoryId == id).ToList();

            if (subCategories.Count() != 0)
            {
                return RedirectToAction("Index");


            }

            db.Categories.Remove(category);
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
