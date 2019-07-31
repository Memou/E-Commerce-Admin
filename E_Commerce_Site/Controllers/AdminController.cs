using E_Commerce_Site.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Site.Controllers
{

    public class AdminController : Controller
    {


        private MainContext db = new MainContext();
        // GET: Admin
        public ActionResult Index() {
            return View();
        }
    }
}