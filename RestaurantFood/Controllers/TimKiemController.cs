using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantFood.Models;

namespace RestaurantFood.Controllers
{
    public class TimKiemController : Controller
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();
        // GET: TimKiem
        public ActionResult TimKiem(string timKiem)
        {

            if (timKiem != null && timKiem != "")
            {
                //var kq = from s in db.SACHes select s;
                var kq = db.MonAn.Where(s => s.Ten_MonAn.Contains(timKiem));
                ViewBag.KQ = kq.Count();
                ViewBag.timKiem = timKiem;
                //var kq = db.SACHes…
                return View(kq);
            }
            return View();
        }
    }
}