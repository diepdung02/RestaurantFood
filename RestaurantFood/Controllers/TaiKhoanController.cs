using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantFood.Models;

namespace RestaurantFood.Controllers
{
    public class TaiKhoanController : Controller
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["Matkhau"];
            var sMatkhauNhapLai = collection["MatkhauNL"];
            var sDiachi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sHoTen))
                ViewData["err1"] = "Họ tên không được rỗng";
            else if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatkhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatkhau != sMatkhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err5"] = "Email không được rỗng";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err6"] = "Số điện thoại không được rỗng";
            }
            else if (db.KHACHHANG.SingleOrDefault(n => n.TaiKhoan == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANG.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
            }
            else if (ModelState.IsValid)
            //Gán giá trị cho đối tượng được tạo mới (kh)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTenDN;
                kh.MatKhau = sMatkhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiachi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANG.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return this.DangKy();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANG.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    Session["HoTen"] = kh.HoTen;
                    return RedirectToAction("Index", "MonAn");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "MonAn");
        }
    }
}