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
    public class ColorsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Colors
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            var colors = db.Colors.AsQueryable();


            if (searchBy == "ColorName")
            {
                colors = colors.Where(x => x.ColorName.StartsWith(search) || search == null);

            }


            ViewBag.SortColorIdParameter = sortBy == "ColorId" ? "ColorId Desc" : "ColorId";
            ViewBag.SortColorNameParameter = sortBy == "ColorName" ? "ColorName Desc" : "ColorName";

            switch (sortBy)
            {
                case "ColorId":
                    colors = colors.OrderBy(x => x.ColorId);
                    break;
                case "ColorId Desc":
                    colors = colors.OrderByDescending(x => x.ColorId);
                    break;
                case "ColorName":
                    colors = colors.OrderBy(x => x.ColorName);
                    break;

                case "ColorName Desc":
                    colors = colors.OrderByDescending(x => x.ColorName);
                    break;

                default:
                    colors = colors.OrderByDescending(x => x.ColorId);
                    break;
            }

            return View(colors.ToPagedList(page ?? 1, 100));







        }


        // GET: Colors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // GET: Colors/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,ColorName")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Colors.Add(color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(color);
        }

        // GET: Colors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,ColorName")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }

        // GET: Colors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get all this colored products
            IEnumerable<Inventory> inventory = db.Inventories.Where(x => x.ColorId == id).ToList();

            if (inventory.Count() != 0) {
                return RedirectToAction("Index");
            }

            //reassignment logic
            //{
            //    if (db.Colors.Any(x => x.ColorName == "Unassigned"))
            //        db.Colors.Add(new Color("Unassigned"));
            //    db.SaveChanges();

            //    Color colorUnassigned = db.Colors.Single(x => x.ColorName == "Unassigned");


            //    foreach (var item in inventory)
            //    {
            //        item.ColorId = colorUnassigned.ColorId;



            //    }


            //    db.SaveChanges();


            //}

            Color color = db.Colors.Find(id);
            db.Colors.Remove(color);
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
