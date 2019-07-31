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
    public class OrdersController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Orders
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            //Search

            var orders = db.Orders.AsQueryable();

            if (searchBy == "OrderId")
            {
                orders = orders.Where(x => x.OrderId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "Status")
            {
                orders = orders.Where(x => x.Status.StartsWith(search) || search == null);

            }

            else if (searchBy == "Created")
            {
                orders = orders.Where(x => x.Created.ToString().Contains(search) || search == null);

            }
            else if (searchBy == "Updated")
            {
                orders = orders.Where(x => x.Updated.ToString().Contains(search) || search == null);

            }
            else if (searchBy == "CustomerName")
            {
                orders = orders.Where(x => x.Customer.CustomerName.StartsWith(search) || search == null);

            }

            //sort
            ViewBag.SortOrderIdParameter = sortBy == "OrderId" ? "OrderIdDesc" : "OrderId";
            ViewBag.SortStatusParameter = sortBy == "Status" ? "StatusDesc" : "Status";
            ViewBag.SortTotalCostParameter = sortBy == "TotalCost" ? "TotalCostDesc" : "TotalCost";
            ViewBag.SortNetCostParameter = sortBy == "NetCost" ? "NetCostDesc" : "NetCost";
            ViewBag.SortTaxCostParameter = sortBy == "TaxCost" ? "TaxCostDesc" : "TaxCost";
            ViewBag.SortCustomerIdParameter = sortBy == "CustomerId" ? "CustomerIdDesc" : "CustomerId";
            ViewBag.SortCustomerNameParameter = sortBy == "CustomerName" ? "CustomerNameDesc" : "CustomerName";
            ViewBag.SortTimeCreatedParameter = sortBy == "TimeCreated" ? "TimeCreatedDesc" : "TimeCreated";
            ViewBag.SortTimeUpdatedParameter = sortBy == "TimeUpdated" ? "TimeUpdatedDesc" : "TimeUpdated";



            switch (sortBy)
            {
                case "OrderId":
                    orders = orders.OrderBy(x => x.OrderId);
                    break;
                case "OrderIdDesc":
                    orders = orders.OrderByDescending(x => x.OrderId);
                    break;
                case "CustomerId":
                    orders = orders.OrderBy(x => x.CustomerId);
                    break;
                case "CustomerIdDesc":
                    orders = orders.OrderByDescending(x => x.Customer.CustomerId);
                    break;
                case "CustomerName":
                    orders = orders.OrderBy(x => x.Customer.CustomerName);
                    break;
                case "CustomerNameDesc":
                    orders = orders.OrderByDescending(x => x.Customer.CustomerName);
                    break;
                case "Status":
                    orders = orders.OrderBy(x => x.Status);
                    break;
                case "StatusDesc":
                    orders = orders.OrderByDescending(x => x.Status);
                    break;
                case "TotalCost":
                    orders = orders.OrderBy(x => x.TotalCost);
                    break;
                case "TotalCostDesc":
                    orders = orders.OrderByDescending(x => x.TotalCost);
                    break;
                case "NetCost":
                    orders = orders.OrderBy(x => x.NetCost);
                    break;
                case "NetCostDesc":
                    orders = orders.OrderByDescending(x => x.NetCost);
                    break;
                case "TaxCost":
                    orders = orders.OrderBy(x => x.TaxCost);
                    break;
                case "TaxCostDesc":
                    orders = orders.OrderByDescending(x => x.TaxCost);
                    break;
                case "TimeCreated":
                    orders = orders.OrderBy(x => x.Created);
                    break;
                case "TimeCreatedDesc":
                    orders = orders.OrderByDescending(x => x.Created);
                    break;
                case "TimeUpdated":
                    orders = orders.OrderBy(x => x.Updated);
                    break;
                case "TimeUpdatedDesc":
                    orders = orders.OrderByDescending(x => x.Updated);
                    break;
                default:
                    orders = orders.OrderByDescending(x => x.OrderId);
                    break;
            }

            return View(orders.ToPagedList(page ?? 1, 100));

        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpGet]
        public ActionResult AddItem(InventoryViewModel inventoryViewModel)
        {
       
            inventoryViewModel.InventoryList = db.Inventories.ToList();
            inventoryViewModel.ColorList = db.Colors.ToList();
            inventoryViewModel.SizeList = db.Sizes.ToList();

           

            //if (inventoryList.Count() == 0  )
            //{
            //    db
            //}

            //if (inventory != null)
            //{

            //    inventoryList.Add(inventory);
            //}

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            //ViewBag.InventoryList = new SelectList(inventoryViewModel.InventoryList, "ProductId", "ProductId") ;
            //ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorId");
            //ViewBag.ProductId = new SelectList(db.Products, "Productid", "ProductName");
            //ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId");


            return View(inventoryViewModel);


        }

        //public JsonResult GetColors(int id) {


        //    List<SelectListItem> colors = new List<SelectListItem>();

        //    List<Inventory> inventory = db.Inventories.Where(x => x.ProductId == id).ToList();




        //    for (int i = 0; i < inventory.Count; i++)
        //    {
        //        inventory.ElementAt(i).ColorId


        //    }
        //    colors.Add(new SelectListItem {Text =  inventory.


        //    })

        //}








        [HttpPost]
        public ActionResult AddItem()
        {

            List<Inventory> inventoryList = new List<Inventory>();


            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ProductId = new SelectList(db.Products, "Productid", "ProductName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(inventoryList);


        }















        //public JsonResult GetColorList(int productId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<Color> ColorList = db.Inventories.Where(x => x.ProductId == productId).ToList();
        //    return Json(StateList, JsonRequestBehavior.AllowGet);

        //}




        public ActionResult CreateProductList()
        {

            List<Product> productList = new List<Product>();


            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ProductId = new SelectList(db.Products, "Productid", "ProductName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(productList);


        }








        // GET: Orders/Create
        public ActionResult Create()
        {
            TempData.Remove("ImageList");
            List<Product> productList = new List<Product>();


            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ProductId = new SelectList(db.Products, "Productid", "ProductName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,TotalCost,NetCost,TaxCost,ShippingCost,Status,OrderNotes,Created,Updated,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated = DateTimeOffset.Now;

                order.Created = DateTimeOffset.Now;

                db.Orders.Add(order);
                db.SaveChanges();


                db.Invoice.Add(new Invoice
                {
                    Printed = false,
                    Canceled = false,
                    Status = "Open",
                    OrderId = order.OrderId
                }
                );
                db.SaveChanges();



                return RedirectToAction("Index");
            }




            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,TotalCost,NetCost,TaxCost,ShippingCost,Status,OrderNotes,Created,Updated,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated = DateTimeOffset.Now;
                db.Entry(order).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
