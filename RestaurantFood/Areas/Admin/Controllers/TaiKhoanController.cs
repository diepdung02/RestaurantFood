using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantFood.Models;

namespace RestaurantFood.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {


        RestaurantFoodEntities db = new RestaurantFoodEntities();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var sTenDN = f["TenDN"];
            var sMatKhau = f["MatKhau"];

            ADMIN ad = db.ADMIN.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }
            return View();
        }

    }
}