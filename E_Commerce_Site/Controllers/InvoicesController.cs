using E_Commerce_Site.Context;
using E_Commerce_Site.Models;
using PagedList;
using Rotativa.MVC;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace E_Commerce_Site.Controllers
{
    public class InvoicesController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Invoices
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            //Search

            var invoices = db.Invoice.AsQueryable();

            if (searchBy == "CustomerName")
            {
                invoices = invoices.Where(x => x.Orders.Customer.CustomerName.StartsWith(search) || search == null);

            }
            else if (searchBy == "InvoiceId")
            {
                invoices = invoices.Where(x => x.InvoiceId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "Status")
            {
                invoices = invoices.Where(x => x.Status.StartsWith(search) || search == null);

            }
            else if (searchBy == "OrderId")
            {
                invoices = invoices.Where(x => x.OrderId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "TimeCreated")
            {
                invoices = invoices.Where(x => x.Created.ToString().Contains(search) || search == null);

            }
            else if (searchBy == "TimeUpdated")
            {
                invoices = invoices.Where(x => x.Updated.ToString().Contains(search) || search == null);

            }

            //sort
            ViewBag.SortInvoiceIdParameter = sortBy == "InvoiceId" ? "InvoiceIdDesc" : "InvoiceId";
            ViewBag.SortStatusParameter = sortBy == "Status" ? "StatusDesc" : "Status";
            ViewBag.SortPrintedParameter = sortBy == "Printed" ? "PrintedDesc" : "Printed";
            ViewBag.SortCanceledParameter = sortBy == "Canceled" ? "CanceledDesc" : "Canceled";
            ViewBag.SortCustomerNameParameter = sortBy == "CustomerName" ? "CustomerNameDesc" : "CustomerName";
            ViewBag.SortOrderIdParameter = sortBy == "OrderId" ? "OrderIdDesc" : "OrderId";
            ViewBag.SortTimeCreatedParameter = sortBy == "TimeCreated" ? "TimeCreatedDesc" : "TimeCreated";
            ViewBag.SortTimeUpdatedParameter = sortBy == "TimeUpdated" ? "TimeUpdatedDesc" : "TimeUpdated";



            switch (sortBy)
            {
                case "InvoiceId":
                    invoices = invoices.OrderBy(x => x.InvoiceId);
                    break;
                case "InvoiceIdDesc":
                    invoices = invoices.OrderByDescending(x => x.InvoiceId);
                    break;
                case "OrderId":
                    invoices = invoices.OrderBy(x => x.OrderId);
                    break;
                case "OrderIdDesc":
                    invoices = invoices.OrderByDescending(x => x.OrderId);
                    break;
                case "CustomerName":
                    invoices = invoices.OrderBy(x => x.Orders.Customer.CustomerName);
                    break;
                case "CustomerNameDesc":
                    invoices = invoices.OrderByDescending(x => x.Orders.Customer.CustomerName);
                    break;
                case "Status":
                    invoices = invoices.OrderBy(x => x.Status);
                    break;
                case "StatusDesc":
                    invoices = invoices.OrderByDescending(x => x.Status);
                    break;
                case "Printed":
                    invoices = invoices.OrderBy(x => x.Printed);
                    break;
                case "PrintedDesc":
                    invoices = invoices.OrderByDescending(x => x.Printed);
                    break;
                case "Canceled":
                    invoices = invoices.OrderBy(x => x.Canceled);
                    break;
                case "CanceledDesc":
                    invoices = invoices.OrderByDescending(x => x.Canceled);
                    break;
                case "TimeCreated":
                    invoices = invoices.OrderBy(x => x.Created);
                    break;
                case "TimeCreatedDesc":
                    invoices = invoices.OrderByDescending(x => x.Created);
                    break;
                case "TimeUpdated":
                    invoices = invoices.OrderBy(x => x.Updated);
                    break;
                case "TimeUpdatedDesc":
                    invoices = invoices.OrderByDescending(x => x.Updated);
                    break;
                default:
                    invoices = invoices.OrderByDescending(x => x.InvoiceId);
                    break;
            }

            return View(invoices.ToPagedList(page ?? 1, 100));

        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,Printed,Canceled,Status,OrderId,Created,Updated")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {

                invoice.Created = DateTimeOffset.Now;
                invoice.Updated = DateTimeOffset.Now;

                db.Invoice.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", invoice.OrderId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", invoice.OrderId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,Printed,Canceled,Status,OrderId,Created,Updated")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.Updated = DateTimeOffset.Now;

                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", invoice.OrderId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoice.Find(id);
            // db.Invoice.Remove(invoice);
            invoice.Canceled = true;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult PreparePrint(int? invoiceId)
        {
            Invoice invoice = db.Invoice.Find(invoiceId);

            return View(invoice);
        }




        // POST: Invoices/Print/5
        public ActionResult Print(int? id)
        {
            return new ActionAsPdf("PreparePrint", new { invoiceId = id });
             
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
