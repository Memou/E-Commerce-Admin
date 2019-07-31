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
    public class CustomersController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Customers
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            //Search

            var customers = db.Customers.AsQueryable();

            if (searchBy == "CustomerName")
            {
                customers = customers.Where(x => x.CustomerName.StartsWith(search) || search == null);

            }
            else if (searchBy == "CustomerId")
            {
                customers = customers.Where(x => x.CustomerId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "Address")
            {
                customers = customers.Where(x => x.Address.StartsWith(search) || search == null);

            }
            else if (searchBy == "PostalCode")
            {
                customers = customers.Where(x => x.PostalCode.StartsWith(search) || search == null);

            }
            else if (searchBy == "PhoneNumber")
            {
                customers = customers.Where(x => x.PhoneNumber.StartsWith(search) || search == null);

            }


            //sort

            ViewBag.SortCustomerIdParameter = sortBy == "CustomerId" ? "CustomerIdDesc" : "CustomerId";
            ViewBag.SortCustomerNameParameter = sortBy == "CustomerName" ? "CustomerNameDesc" : "CustomerName";
            ViewBag.SortAddressParameter = sortBy == "Address" ? "AddressDesc" : "Address";
            ViewBag.SortCityParameter = sortBy == "City" ? "CityDesc" : "City";
            ViewBag.SortStateParameter = sortBy == "State" ? "StateDesc" : "State";
            ViewBag.SortCountryParameter = sortBy == "Country" ? "CountryDesc" : "Country";
            ViewBag.SortPostalCodeParameter = sortBy == "PostalCode" ? "PostalCodeDesc" : "PostalCode";
            ViewBag.SortPhoneNumberParameter = sortBy == "PhoneNumber" ? "PhoneNumberDesc" : "PhoneNumber";
            ViewBag.SortDateOfBirthParameter = sortBy == "DateOfBirth" ? "DateOfBirthDesc" : "DateOfBirth";
            ViewBag.SortTimeCreatedParameter = sortBy == "TimeCreated" ? "TimeCreatedDesc" : "TimeCreated";
            ViewBag.SortTimeUpdatedParameter = sortBy == "TimeUpdated" ? "TimeUpdatedDesc" : "TimeUpdated";

            

            switch (sortBy)
            {
                case "CustomerId":
                    customers = customers.OrderBy(x => x.CustomerId);
                    break;
                case "CustomerIdDesc":
                    customers = customers.OrderByDescending(x => x.CustomerId);
                    break;
                case "CustomerName":
                    customers = customers.OrderBy(x => x.CustomerName);
                    break;
                case "CustomerNameDesc":
                    customers = customers.OrderByDescending(x => x.CustomerName);
                    break;
                case "Address":
                    customers = customers.OrderBy(x => x.Address);
                    break;
                case "AddressDesc":
                    customers = customers.OrderByDescending(x => x.Address);
                    break;
                case "Country":
                    customers = customers.OrderBy(x => x.Country);
                    break;
                case "CountryDesc":
                    customers = customers.OrderByDescending(x => x.Country);
                    break;
                case "City":
                    customers = customers.OrderBy(x => x.City);
                    break;
                case "CityDesc":
                    customers = customers.OrderByDescending(x => x.City);
                    break;
                case "State":
                    customers = customers.OrderBy(x => x.State);
                    break;
                case "StateDesc":
                    customers = customers.OrderByDescending(x => x.State);
                    break;
                case "PhoneNumber":
                    customers = customers.OrderBy(x => x.PhoneNumber);
                    break;
                case "PhoneNumberDesc":
                    customers = customers.OrderByDescending(x => x.PhoneNumber);
                    break;
                case "PostalCode":
                    customers = customers.OrderBy(x => x.PostalCode);
                    break;
                case "PostalCodeDesc":
                    customers = customers.OrderByDescending(x => x.PostalCode);
                    break;
                case "DateOfBirth":
                    customers = customers.OrderBy(x => x.DateOfBirth);
                    break;
                case "DateOfBirthDesc":
                    customers = customers.OrderByDescending(x => x.DateOfBirth);
                    break;
                case "TimeUpdated":
                    customers = customers.OrderBy(x => x.Updated);
                    break;
                case "TimeUpdatedDesc":
                    customers = customers.OrderByDescending(x => x.Updated);
                    break;
                case "TimeCreated":
                    customers = customers.OrderBy(x => x.Created);
                    break;
                case "TimeCreatedDesc":
                    customers = customers.OrderByDescending(x => x.Created);
                    break;


                default:
                    customers = customers.OrderByDescending(x => x.CustomerId);
                    break;
            }

            return View(customers.ToPagedList(page ?? 1, 100));







        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CustomerName,Address,PostalCode,Country,City,State,PhoneNumber,DateOfBirth,CustomerNotes,Created,Updated,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Updated = DateTimeOffset.Now;
                customer.Created = DateTimeOffset.Now;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerName,Address,PostalCode,Country,City,State,PhoneNumber,DateOfBirth,CustomerNotes,Created,Updated,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
               db.Entry(customer).State = EntityState.Modified;
                customer.Updated = DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
