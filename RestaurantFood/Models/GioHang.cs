using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantFood.Models;

namespace RestaurantFood.Models
{
    public class GioHang
    {
        RestaurantFoodEntities db = new RestaurantFoodEntities();

        public int iID_MonAn { get; set; }
        public string sTen_MonAn { get; set; }
        public string sAnhChinh_MonAn { get; set; }
        public double dGiaBanMoi_MonAn { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien {
            get { return iSoLuong * dGiaBanMoi_MonAn; }
        }


        public GioHang (int id_MonAn)   
        {
            iID_MonAn = id_MonAn;
            MonAn monAn = db.MonAn.Single(n => n.Id_MonAn == iID_MonAn);
            sTen_MonAn = monAn.Ten_MonAn;
            sAnhChinh_MonAn = monAn.AnhChinh_MonAn;
            dGiaBanMoi_MonAn = Convert.ToDouble(monAn.GiaBanMoi_MonAn);
            iSoLuong = 1;
        }
    }
}