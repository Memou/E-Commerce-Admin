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
    public class TagsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Tags
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");

            var tag = db.Tags.AsQueryable();

            if (searchBy == "TagId")
            {
                tag = tag.Where(x => x.TagId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "TagName")
            {
                tag = tag.Where(x => x.TagName.StartsWith(search) || search == null);

            }
   

            ViewBag.SortTagIdParameter = sortBy == "TagId" ? "TagId Desc" : "TagId";
            ViewBag.SortTagNameParameter = sortBy == "TagName" ? "TagName Desc" : "TagName";

            switch (sortBy)
            {
                case "TagId":
                    tag = tag.OrderBy(x => x.TagId);
                    break;
                case "TagId Desc":
                    tag = tag.OrderByDescending(x => x.TagId);
                    break;
                case "TagName":
                    tag = tag.OrderBy(x => x.TagId);
                    break;

                case "TagName Desc":
                    tag = tag.OrderByDescending(x => x.TagName);
                    break;

                default:
                    tag = tag.OrderByDescending(x => x.TagId);
                    break;
            }

            return View(tag.ToPagedList(page ?? 1, 100));

        }


        // GET: Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,TagName")] Tag tag)
        {TempData.Remove("ImageList");
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,TagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
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
