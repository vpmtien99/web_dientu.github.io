using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult Index(string ten, string diachi, string SDT)
        {
            var db = new KinhContext();
            HoaDonDAO dao = new HoaDonDAO();
            var hd = Session["HD"] as HoaDon;
            var list = new List<Product>();
            var tempHD = db.HoaDons.Find(hd.MaHD);

            tempHD.DiaChi = diachi;
            tempHD.SDT = SDT;
            tempHD.TenNguoiNhan = ten;
            double tong = 0;

            double phanTram = 0;
            foreach (var item in db.TichLuys.OrderByDescending(i => i.Diem).ToList())
            {
                if (db.KhachHangs.Find(hd.KHId).DiemTichLuy >= item.Diem)
                {
                    phanTram = item.PhanTram;
                    break;
                }
            }

            var ls = dao.GetListCTHD(hd.MaHD);
            foreach (var item in ls)
            {
                tong = tong + item.ThanhTien;
                Product temp = new Product();
                temp.item_number = item.KinhId;
                temp.item_name = item.Kinh.TenKinh;
                temp.quantity = item.SoLuong / item.SoLuong;
                double thanhTien = item.ThanhTien - (item.ThanhTien * ((phanTram * 1.0) / 100));
                temp.amount = Math.Round(((thanhTien) * 1.0) / (1 * 1.0), MidpointRounding.ToEven);
                list.Add(temp);
            }
            tempHD.TongTien = Math.Round((tong - (tong * ((7 * 1.0) / 100))), MidpointRounding.ToEven);
            db.SaveChanges();
            return View(list);
        }
        public ActionResult GetDataPaypal()
        {
            var db = new KinhContext();
            var hd = Session["HD"] as HoaDon;
            var tempHD = db.HoaDons.Find(hd.MaHD);
            tempHD.TinhTrangHoaDon = "Đã Thanh Toán";
            tempHD.TinhTrangDonHang = "Đang Giao Hàng";
            tempHD.NgayTao = DateTime.Now;
            foreach (var item in db.ChiTietHoaDons.Where(i => i.HDId == hd.MaHD).ToList())
            {
                var mh = db.Kinhs.Find(item.KinhId);
                mh.SoLuong = mh.SoLuong - item.SoLuong;
                db.SaveChanges();
            }
            db.SaveChanges();
            Session["HD"] = null;
            Session["ListCTHD"] = null;
            ViewBag.Err = "Thanh toán thành công";
            return View("../Home/Index");
        }
    }
}