using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
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
                    KhachHangDAO dao = new KhachHangDAO();
                    Session["ListKH"] = dao.GetList();
                    Session["ListKHBlock"] = dao.GetListBlock();

                    return View();
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
                    KhachHangDAO dao = new KhachHangDAO();
                    Session["ListKH"] = dao.GetList();
                    Session["ListKHBlock"] = dao.GetListBlock();
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult TimKiem(string username, string ho, string ten, string tinhtrang, string diem, string ss)
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
                    KhachHangDAO dao = new KhachHangDAO();
                    if (tinhtrang == "Unblock")
                    {
                        Session["ListKH"] = dao.Search(username, ho, ten, diem, ss);
                    }
                    else
                    {
                        Session["ListKHBlock"] = dao.SearchBlock(username, ho, ten, diem, ss);
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idKH)
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
                    KhachHangDAO dao = new KhachHangDAO();
                    var admin = dao.GetKH(idKH);
                    if (admin == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy tài khoản";
                    }
                    else
                    {
                        Session["UpdateKH"] = admin;
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult UnBlock(bool confirm, int idKH)
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
                    if (confirm == true)
                    {
                        
                        KhachHangDAO dao = new KhachHangDAO();
                        dao.UnBlock(idKH, tk.MaKH);
                        Session["ListKH"] = dao.GetList();
                        Session["ListKHBlock"] = dao.GetListBlock();
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Block(bool confirm, int idKH)
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
                    if (confirm == true)
                    {
                       
                        KhachHangDAO dao = new KhachHangDAO();
                        dao.Block(idKH, tk.MaKH);
                        if (Session["UpdateKH"] != null)
                        {
                            var admin = Session["UpdateKH"] as KhachHang;
                            if (admin.MaKH == idKH)
                            {
                                Session["UpdateKH"] = null;
                            }
                        }
                        Session["ListKH"] = dao.GetList();
                        Session["ListKHBlock"] = dao.GetListBlock();
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult Index(string username, string ho, string ten, DateTime ngaysinh, string gioitinh)
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
                    KhachHangDAO dao = new KhachHangDAO();
                    if (Session["UpdateKH"] != null)
                    {
                        
                        var admin = Session["UpdateKH"] as KhachHang;
                        dao.UpdateKH(tk.MaKH, admin.MaKH, ho, ten, ngaysinh, gioitinh);
                        Session["ListKH"] = dao.GetList();
                        Session["UpdateKH"] = null;
                    }
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