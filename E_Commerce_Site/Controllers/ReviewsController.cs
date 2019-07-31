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
    public class ReviewsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Reviews
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            //Search

            var reviews = db.Reviews.AsQueryable();

            if (searchBy == "CustomerName")
            {
                reviews = reviews.Where(x => x.Customer.CustomerName.StartsWith(search) || search == null);

            }
            else if (searchBy == "CustomerId")
            {
                reviews = reviews.Where(x => x.CustomerId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "ProductId")
            {
                reviews = reviews.Where(x => x.ProductId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "ProductName")
            {
                reviews = reviews.Where(x => x.Product.ProductName.StartsWith(search) || search == null);

            }
            else if (searchBy == "ReviewId")
            {
                reviews = reviews.Where(x => x.ReviewId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "Review")
            {
                reviews = reviews.Where(x => x.ReviewString.Contains(search) || search == null);

            }

            //sort
            ViewBag.SortReviewIdParameter = sortBy == "ReviewId" ? "ReviewIdDesc" : "ReviewId";
            ViewBag.SortCustomerIdParameter = sortBy == "CustomerId" ? "CustomerIdDesc" : "CustomerId";
            ViewBag.SortCustomerNameParameter = sortBy == "CustomerName" ? "CustomerNameDesc" : "CustomerName";
            ViewBag.SortProductIdParameter = sortBy == "ProductId" ? "ProductIdDesc" : "ProductId";
            ViewBag.SortProductNameParameter = sortBy == "ProductName" ? "ProductNameDesc" : "ProductName";
            ViewBag.SortReviewParameter = sortBy == "Review" ? "ReviewDesc" : "Review";
            ViewBag.SortTimeCreatedParameter = sortBy == "TimeCreated" ? "TimeCreatedDesc" : "TimeCreated";
            ViewBag.SortTimeUpdatedParameter = sortBy == "TimeUpdated" ? "TimeUpdatedDesc" : "TimeUpdated";
            ViewBag.SortRatingParameter = sortBy == "Rating" ? "RatingDesc" : "Rating";



            switch (sortBy)
            {
                case "CustomerId":
                    reviews = reviews.OrderBy(x => x.CustomerId);
                    break;
                case "CustomerIdDesc":
                    reviews = reviews.OrderByDescending(x => x.CustomerId);
                    break;
                case "CustomerName":
                    reviews = reviews.OrderBy(x => x.Customer.CustomerName);
                    break;
                case "CustomerNameDesc":
                    reviews = reviews.OrderByDescending(x => x.Customer.CustomerName);
                    break;
                case "ProductId":
                    reviews = reviews.OrderBy(x => x.ProductId);
                    break;
                case "ProductIdDesc":
                    reviews = reviews.OrderByDescending(x => x.ProductId);
                    break;
                case "ProductName":
                    reviews = reviews.OrderBy(x => x.Product.ProductName);
                    break;
                case "ProductNameDesc":
                    reviews = reviews.OrderByDescending(x => x.Product.ProductName);
                    break;
                case "Review":
                    reviews = reviews.OrderBy(x => x.ReviewString);
                    break;
                case "ReviewDesc":
                    reviews = reviews.OrderByDescending(x => x.ReviewString);
                    break;
                case "TimeCreated":
                    reviews = reviews.OrderBy(x => x.Created);
                    break;
                case "TimeCreatedDesc":
                    reviews = reviews.OrderByDescending(x => x.Created);
                    break;
                case "TimeUpdated":
                    reviews = reviews.OrderBy(x => x.Updated);
                    break;
                case "TimeUpdatedDesc":
                    reviews = reviews.OrderByDescending(x => x.Updated);
                    break;
                case "Rating":
                    reviews = reviews.OrderBy(x => x.Rating);
                    break;
                case "RatingDesc":
                    reviews = reviews.OrderByDescending(x => x.Rating);
                    break;
                default:
                    reviews = reviews.OrderByDescending(x => x.ReviewId);
                    break;
            }

            return View(reviews.ToPagedList(page ?? 1, 100));

        }

            // GET: Reviews/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewId,ReviewString,Created,Updated,ProductId,CustomerId,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Created = DateTimeOffset.Now;
                review.Updated = DateTimeOffset.Now;

                db.Reviews.Add(review);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", review.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", review.ProductId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", review.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", review.ProductId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewId,ReviewString,Created,Updated,Rating,ProductId,CustomerId")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Updated = DateTimeOffset.Now;
                db.Entry(review).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", review.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", review.ProductId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
