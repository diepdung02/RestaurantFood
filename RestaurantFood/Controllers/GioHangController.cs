using RestaurantFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantFood.Controllers
{
    public class GioHangController : Controller
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CapNhatGioHang(int id_MonAn, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iID_MonAn == id_MonAn);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "MonAn");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DonDatHang ddh = new DonDatHang();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> lstGioHang = LayGioHang();
            int id_ddh = db.DonDatHang.Count() + 1;
            ddh.Id_DonDatHang = id_ddh;
            ddh.DaThanhToan = false;
            ddh.TinhTrangGiao = true;
            ddh.NgayDat = DateTime.Now;
            var Giao = DateTime.Now.AddDays(7).ToString();
            var NgayGiao = string.Format("{0:MM/dd/yyyy}", Giao);
            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.MaKH = kh.MaKH;
            db.DonDatHang.Add(ddh);
            db.SaveChanges();
            // them chi tiet don hang
            foreach (var item in lstGioHang)
            {
                ChiTietDatHang ctdh = new ChiTietDatHang();
                ctdh.Id_DonDatHang = ddh.Id_DonDatHang;
                ctdh.Id_MonAn = item.iID_MonAn;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dGiaBanMoi_MonAn;
                db.ChiTietDatHang.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }


        public ActionResult GioHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.Count = lstGioHang.Count;
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);

        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        public ActionResult XoaSPKhoiGioHang(int id_MonAn)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iID_MonAn == id_MonAn);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iID_MonAn == id_MonAn);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "MonAn");
                }
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang()
        {
            List<GioHang> lst = LayGioHang();
            lst.Clear();
            return RedirectToAction("GioHang");
        }
        public ActionResult ThemGioHang(int id_MonAn, string url)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.iID_MonAn == id_MonAn);
            if (sp == null)
            {
                sp = new GioHang(id_MonAn);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }

            return Redirect(url);
        }

        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        public double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
    }
}