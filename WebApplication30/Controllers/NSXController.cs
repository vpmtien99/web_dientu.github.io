using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class NSXController : Controller
    {
        // GET: NSX
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
                    NSXDAO dao = new NSXDAO();
                    Session["ListNSX"] = dao.GetList();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idNSX)
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
                    NSXDAO dao = new NSXDAO();
                    var nsx = dao.GetNSX(idNSX);
                    if (nsx == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy nhà sản xuất";
                    }
                    else
                    {
                        Session["UpdateNSX"] = nsx;
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult TimKiem(string tennsx)
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
                    NSXDAO dao = new NSXDAO();
                    Session["ListNSX"] = dao.Search(tennsx);
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Delete(bool confirm, int idNSX)
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

                        NSXDAO dao = new NSXDAO();
                        if (dao.DeleteNSX(idNSX, tk.MaKH) == false)
                        {
                            ViewBag.Alert = "Không thể xóa nhà sản xuất này";
                        }
                        else
                        {
                            if (Session["UpdateNSX"] != null)
                            {
                                var nsx = Session["UpdateNSX"] as NSX;
                                if (nsx.MaNSX == idNSX)
                                {
                                    Session["UpdateNSX"] = null;
                                }
                            }
                            Session["ListNSX"] = dao.GetList();
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
        public ActionResult Index(string tennsx, string mota, HttpPostedFileBase anh)
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
                    NSXDAO dao = new NSXDAO();
                    if (Session["UpdateNSX"] == null)
                    {

                        if (dao.ThemNSX(tk.MaKH, tennsx, anh, mota) == false)
                        {
                            ViewBag.Err = "Tên nhà sản xuất đã tồn tại";
                        }
                        else
                        {
                            Session["ListNSX"] = dao.GetList();
                        }
                    }
                    else
                    {
                        var nsx = Session["UpdateNSX"] as NSX;
                        if (dao.UpdateNSX(tk.MaKH, nsx.MaNSX, tennsx, anh, mota) == false)
                        {
                            ViewBag.Err = "Tên nhà sản xuất đã tồn tại";
                        }
                        else
                        {
                            Session["UpdateNSX"] = null;
                            Session["ListNSX"] = dao.GetList();
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