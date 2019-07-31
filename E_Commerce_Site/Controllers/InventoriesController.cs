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
    public class InventoriesController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Inventories
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            TempData.Remove("ImageList");
            var inventories = db.Inventories.Include(i => i.Color).Include(i => i.Product).Include(i => i.Size);
            //Search


            if (searchBy == "ProductId")
            {
                inventories = inventories.Where(x => x.ProductId.ToString().StartsWith(search) || search == null);

            }
            else if (searchBy == "ProductName")
            {
                inventories = inventories.Where(x => x.Product.ProductName.StartsWith(search) || search == null);

            }

            //sort

            ViewBag.SortInventoryIdParameter = sortBy == "InventoryId" ? "InventoryId Desc" : "InventoryId";
            ViewBag.SortProductIdParameter = sortBy == "ProductId" ? "ProductId Desc" : "ProductId";
            ViewBag.SortProductNameParameter = sortBy == "ProductName" ? "ProductName Desc" : "ProductName";
            ViewBag.SortColorParameter = sortBy == "Color" ? "Color Desc" : "Color";
            ViewBag.SortSizeParameter = sortBy == "Size" ? "Size Desc" : "Size";
            ViewBag.SortQuantityParameter = sortBy == "Quantity" ? "Quantity Desc" : "Quantity";



            switch (sortBy)
            {
                case "InventoryId":
                    inventories = inventories.OrderBy(x => x.InventoryId);
                    break;
                case "InventoryId Desc":
                    inventories = inventories.OrderByDescending(x => x.InventoryId);
                    break;
                case "ProductId":
                    inventories = inventories.OrderBy(x => x.ProductId);
                    break;
                case "ProductId Desc":
                    inventories = inventories.OrderByDescending(x => x.ProductId);
                    break;
                case "ProductName":
                    inventories = inventories.OrderBy(x => x.Product.ProductName);
                    break;
                case "ProductName Desc":
                    inventories = inventories.OrderByDescending(x => x.Product.ProductName);
                    break;
                case "Color":
                    inventories = inventories.OrderBy(x => x.Color.ColorName);
                    break;
                case "Color Desc":
                    inventories = inventories.OrderByDescending(x => x.Color.ColorName);
                    break;
                case "Size":
                    inventories = inventories.OrderBy(x => x.Size.SizeName);
                    break;
                case "Size Desc":
                    inventories = inventories.OrderByDescending(x => x.Size.SizeName);
                    break;
                case "Quantity":
                    inventories = inventories.OrderBy(x => x.Quantity);
                    break;
                case "Quantity Desc":
                    inventories = inventories.OrderByDescending(x => x.Quantity);
                    break;

                default:
                    inventories = inventories.OrderByDescending(x => x.InventoryId);
                    break;
            }

            return View(inventories.ToPagedList(page ?? 1, 100));

        }


        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create(int? productid)
        {
            TempData.Remove("ImageList");







            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ProductId = new SelectList(db.Products, "Productid", "ProductName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            ViewBag.SpecificId = productid;
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryId,ProductId,SizeId,ColorId,Quantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {

                var snventory = db.Inventories.Where(x => x.ProductId == inventory.ProductId);
                snventory = snventory.Where(x => x.SizeId == inventory.SizeId);
                snventory = snventory.Where(x => x.ColorId == inventory.ColorId);

                //better written:
                //var sss = db.Inventories.Where(x => x.ProductId == inventory.ProductId && x.SizeId.Equals(inventory.Size) && x.ColorId.Equals(inventory.ColorId));


                if (snventory.Count() != 0)
                {
                    Inventory i1 = db.Inventories.Single(m => m.ProductId.Equals(inventory.ProductId) && m.SizeId.Equals(inventory.SizeId) && m.ColorId.Equals(inventory.ColorId));
                    i1.Quantity += inventory.Quantity;

                }

                else
                {
                    db.Inventories.Add(inventory);
                }
                db.SaveChanges();

                return RedirectToAction("Create");
            }

            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", inventory.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", inventory.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", inventory.SizeId);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory inventory = db.Inventories.Find(id);

            if (inventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", inventory.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", inventory.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", inventory.SizeId);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryId,ProductId,SizeId,ColorId,Quantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", inventory.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", inventory.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", inventory.SizeId);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
