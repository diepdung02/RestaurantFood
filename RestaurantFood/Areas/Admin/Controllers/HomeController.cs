using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantFood.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult Navbar()
        {
            return PartialView();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }

        public ActionResult Spinnerbar()
        {
            return PartialView();
        }
    }
}