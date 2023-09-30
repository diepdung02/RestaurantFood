using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantFood.Models;


namespace RestaurantFood.Controllers
{
    public class HomeController : Controller
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();
        public ActionResult Banner()
        {
            return PartialView();
        }
        public ActionResult Index(int? page)
        {
            List<MonAn> ketQua = db.MonAn.ToList();
            return View(ketQua);
        }


        public ActionResult DanhMucThucDon()
        {
            List<ThucDon> ketQua = db.ThucDon.ToList();
            return PartialView(ketQua);
        }

        public ActionResult DanhMucLoaiMon()
        {
            List<LoaiMon> ketQua = db.LoaiMon.ToList();
            return PartialView(ketQua);
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult LoadChildMenu(int parentId)
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENU.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumber).ToList();
            ViewBag.Count = lst.Count();
            int[] a = new int[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENU.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView("LoadChildMenu", lst);
        }

        public ActionResult FromTheBlog()
        {
            return PartialView();
        }
        public ActionResult ThanhSanPham()
        {
            return PartialView();
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENU.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumber).ToList();
            int[] a = new int[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENU.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView(lst);
        }
    }
}