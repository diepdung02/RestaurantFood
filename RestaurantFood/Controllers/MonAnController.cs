using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantFood.Models;
using PagedList;

namespace RestaurantFood.Controllers
{
    public class MonAnController : Controller
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();
        // GET: MonAn
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var lst = db.MonAn.OrderBy(x => x.Id_MonAn);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 6;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(lst.ToPagedList(pageNumber, pageSize));
        }

          public ActionResult ChiTietSanPham(int id)
        {
            MonAn ketQua = db.MonAn.Find(id);
            LoaiMon loaiMon = db.LoaiMon.Find(ketQua.Id_LoaiMon);
            ViewBag.loaiMon = loaiMon.Ten_LoaiMon;
            ThucDon thucDon = db.ThucDon.Find(ketQua.Id_ThucDon);
            ViewBag.thucDon = thucDon.Ten_ThucDon;
            ViewBag.lstCungLoai = db.MonAn.Where(s => s.Id_LoaiMon == ketQua.Id_LoaiMon).ToList();
            return View(ketQua);
        }
        public ActionResult DanhSachSanPham(int? page)
        {
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var lst = db.MonAn.OrderBy(x => x.Id_MonAn);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(lst.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult XemTheoLoaiMon(string tenLoai, int id, int? page)
        {
            List<MonAn> ketQua = db.MonAn.Where(s => s.Id_LoaiMon == id).ToList();
            ViewBag.tenLoai = tenLoai;
            ViewBag.page = page ?? 1;
            return View(ketQua);
        }

        public ActionResult XemTheoLoaiThucDon(string tenLoai, int id, int? page)
        {
            List<MonAn> ketQua = db.MonAn.Where(s => s.Id_ThucDon == id).OrderBy(s => s.Id_LoaiMon).ToList();
            ViewBag.tenLoai = tenLoai;
            ViewBag.page = page ?? 1;
            return View(ketQua);
        }

        public ActionResult GiamGia()
        {
            return PartialView();
        }
        public ActionResult MoiNhat()
        {
            List<MonAn> ketQua = db.MonAn.OrderByDescending(m => m.Id_MonAn).Take(6).ToList();
            return PartialView(ketQua);
        }

        public ActionResult BanNhieuNhat()
        {
            List<MonAn> ketQua = db.MonAn.OrderByDescending(m => m.SoLuongBan).Take(6).ToList();
            return PartialView(ketQua);
        }


    }
}