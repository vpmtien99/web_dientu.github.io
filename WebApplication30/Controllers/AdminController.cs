using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
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
                    AdminDAO dao = new AdminDAO();
                    Session["ListAdmin"] = dao.GetList();
                    Session["ListAdminBlock"] = dao.GetListBlock();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Chart()
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
                    return View("Chart");
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
                    AdminDAO dao = new AdminDAO();
                    Session["ListAdmin"] = dao.GetList();
                    Session["ListAdminBlock"] = dao.GetListBlock();
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult TimKiem(string username, string ho, string ten, string tinhtrang)
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
                    AdminDAO dao = new AdminDAO();
                    if (tinhtrang == "Unblock")
                    {
                        Session["ListAdmin"] = dao.Search(username, ho, ten);
                    }
                    else
                    {
                        Session["ListAdminBlock"] = dao.SearchBlock(username, ho, ten);
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idAdmin)
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
                    AdminDAO dao = new AdminDAO();
                    var admin = dao.GetAdmin(idAdmin);
                    if (admin == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy tài khoản";
                    }
                    else
                    {
                        Session["UpdateAdmin"] = admin;
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult UnBlock(bool confirm, int idAdmin)
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
                        AdminDAO dao = new AdminDAO();
                        dao.UnBlock(idAdmin, tk.MaKH);
                        Session["ListAdmin"] = dao.GetList();
                        Session["ListAdminBlock"] = dao.GetListBlock();
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Block(bool confirm, int idAdmin)
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

                        AdminDAO dao = new AdminDAO();
                        dao.Block(idAdmin, tk.MaKH);
                        if (Session["UpdateAdmin"] != null)
                        {
                            var admin = Session["UpdateAdmin"] as KhachHang;
                            if (admin.MaKH == idAdmin)
                            {
                                Session["UpdateAdmin"] = null;
                            }
                        }
                        Session["ListAdmin"] = dao.GetList();
                        Session["ListAdminBlock"] = dao.GetListBlock();
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
                   
                    AdminDAO dao = new AdminDAO();
                    if (Session["UpdateAdmin"] == null)
                    {
                        if (dao.AddKH(tk.MaKH, username, ho, ten, ngaysinh, gioitinh) == false)
                        {
                            ViewBag.Err = "Username đã được sử dụng";
                        }
                        else
                        {
                            Session["ListAdmin"] = dao.GetList();
                        }
                    }
                    else
                    {
                        var admin = Session["UpdateAdmin"] as KhachHang;
                        dao.UpdateKH(tk.MaKH, admin.MaKH, ho, ten, ngaysinh, gioitinh);
                        Session["ListAdmin"] = dao.GetList();
                        Session["UpdateAdmin"] = null;
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