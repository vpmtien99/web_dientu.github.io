using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var db = new KinhContext();
            Log log = new Log();
            log.KHId = 3;
            log.KhachHang = db.KhachHangs.Find(3);
            log.Ngay = DateTime.Now;
            log.HanhDong = "TKhachHang";
            log.ChiTiet = "ádasfas";
            db.Logs.Add(log);
            db.SaveChanges();
            return View();
        }

        public ActionResult MuaNgay(int idMH)
        {
            HoaDonDAO dao = new HoaDonDAO();
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                var hd = dao.FindHD(tk.MaKH);
                if (hd == null)
                {
                    if (dao.CreateHD(tk.MaKH, idMH) == false)
                    {
                        ViewBag.Err = "Số lượng không đủ";
                    }
                    else
                    {
                        ViewBag.Err = "Thêm thành công";
                    }
                }
                else
                {
                    if (dao.UpdateHD(hd.MaHD, idMH) == false)
                    {
                        ViewBag.Err = "Số lượng không đủ";
                    }
                    else
                    {
                        ViewBag.Err = "Thêm thành công";
                    }
                }
                return RedirectToAction("Index", "GioHang");
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }

        public ActionResult TimKiem(string tenMH)
        {
            var db = new KinhContext();
            Session["Title"] = "Tìm Kiếm Theo Tên: " + tenMH;
            Session["Products"] = db.Kinhs.Where(i => i.Flag == true && i.TenKinh.Contains(tenMH)).ToList();
            return RedirectToAction("Index", "MoHinhs");
        }
    }
}