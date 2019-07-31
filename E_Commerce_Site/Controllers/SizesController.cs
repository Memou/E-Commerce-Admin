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
    public class SizesController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Sizes
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            var sizes = db.Sizes.AsQueryable();


            if (searchBy == "SizeName")
            {
                sizes = sizes.Where(x => x.SizeName.StartsWith(search) || search == null);

            }


            ViewBag.SortSizeIdParameter = sortBy == "SizeId" ? "SizeId Desc" : "SizeId";
            ViewBag.SortSizeNameParameter = sortBy == "SizeName" ? "SizeName Desc" : "SizeName";

            switch (sortBy)
            {
                case "SizeId":
                    sizes = sizes.OrderBy(x => x.SizeId);
                    break;
                case "SizeId Desc":
                    sizes = sizes.OrderByDescending(x => x.SizeId);
                    break;
                case "SizeName":
                    sizes = sizes.OrderBy(x => x.SizeName);
                    break;

                case "SizeName Desc":
                    sizes = sizes.OrderByDescending(x => x.SizeName);
                    break;

                default:
                    sizes = sizes.OrderByDescending(x => x.SizeId);
                    break;
            }
            return View(sizes.ToPagedList(page ?? 1, 100));
        }

        // GET: Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // GET: Sizes/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeId,SizeName")] Size size)
        {
            if (ModelState.IsValid)
            {
                db.Sizes.Add(size);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(size);
        }

        // GET: Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeId,SizeName")] Size size)
        {
            if (ModelState.IsValid)
            {
                db.Entry(size).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(size);
        }

        // GET: Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            IEnumerable<Inventory> inventory = db.Inventories.Where(x => x.SizeId == id).ToList();

            if (inventory.Count() != 0)
            {
                return RedirectToAction("Index");
            }

            Size size = db.Sizes.Find(id);

            db.Sizes.Remove(size);
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
