using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class LoaiController : Controller
    {
        // GET: Loai
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
                    LoaiDAO dao = new LoaiDAO();
                    Session["ListLoai"] = dao.GetList();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idLoai)
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
                    LoaiDAO dao = new LoaiDAO();
                    var nsx = dao.GetLoai(idLoai);
                    if (nsx == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy loại";
                    }
                    else
                    {
                        Session["UpdateLoai"] = nsx;
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult TimKiem(string tenloai)
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
                    LoaiDAO dao = new LoaiDAO();
                    Session["ListLoai"] = dao.Search(tenloai);
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Delete(bool confirm, int idLoai)
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

                        LoaiDAO dao = new LoaiDAO();
                        if (dao.DeleteLoai(idLoai, tk.MaKH) == false)
                        {
                            ViewBag.Alert = "Không thể xóa loại này";
                        }
                        else
                        {
                            if (Session["UpdateLoai"] != null)
                            {
                                var nsx = Session["UpdateLoai"] as Loai;
                                if (nsx.MaLoai == idLoai)
                                {
                                    Session["UpdateLoai"] = null;
                                }
                            }
                            Session["ListLoai"] = dao.GetList();
                        }
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
        public ActionResult Index(string tenloai)
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
                   
                    LoaiDAO dao = new LoaiDAO();
                    if (Session["UpdateLoai"] == null)
                    {

                        if (dao.ThemLoai(tk.MaKH, tenloai) == false)
                        {
                            ViewBag.Err = "Tên loại đã tồn tại";
                        }
                        else
                        {
                            Session["ListLoai"] = dao.GetList();
                        }
                    }
                    else
                    {
                        var nsx = Session["UpdateLoai"] as Loai;
                        if (dao.UpdateLoai(tk.MaKH, nsx.MaLoai, tenloai) == false)
                        {
                            ViewBag.Err = "Tên loại đã tồn tại";
                        }
                        else
                        {
                            Session["UpdateLoai"] = null;
                            Session["ListLoai"] = dao.GetList();
                        }

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