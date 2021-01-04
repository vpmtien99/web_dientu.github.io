using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult Search(string loai, string taikhoan, DateTime? ngay, string hanhdong)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    LogDAO dao = new LogDAO();
                    Session["Logs"] = dao.Search(loai, taikhoan, ngay, hanhdong);
                    return View("Index");

                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Reset()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    LogDAO dao = new LogDAO();
                    string loai = Session["LoaiLog"] as string;
                    Session["Logs"] = dao.GetListLog(loai);
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult KhuyenMaiLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "KhuyenMai";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = null;
                    Session["Logs"] = dao.GetListLog("KhuyenMai");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult HoaDonLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "HoaDon";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = "HoaDon";
                    Session["Logs"] = dao.GetListLog("HoaDon");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult LoaiLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "Loai";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = null;
                    Session["Logs"] = dao.GetListLog("Loai");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult NSXLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "NSX";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = null;
                    Session["Logs"] = dao.GetListLog("NSX");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult AllLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "TruyCap";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = "TruyCap";
                    Session["Logs"] = dao.GetListLog("TruyCap");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult MoHinhLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "MoHinh";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = null;
                    Session["Logs"] = dao.GetListLog("MoHinh");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult KhachHangLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "KhachHang";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = "KH";
                    Session["Logs"] = dao.GetListLog("KhachHang");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult AdminLog()
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;


                if (tk.LoaiTK == "Khách Hàng")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["LoaiLog"] = "Admin";
                    LogDAO dao = new LogDAO();
                    Session["TKLog"] = "Admin";
                    Session["Logs"] = dao.GetListLog("Admin");
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
    }
}