using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                HoaDonDAO dao = new HoaDonDAO();
                var tk = Session["TaiKhoan"] as KhachHang;
                var hd = dao.FindHD(tk.MaKH);
                if (hd != null)
                {
                    Session["ListCTHD"] = dao.GetListCTHD(hd.MaHD);
                }
            }
            return View();
        }
      
        [HttpPost]
        public ActionResult UpdateSL(int idCTHD, int sl)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                HoaDonDAO dao = new HoaDonDAO();
                int rs = dao.UpdateSL(idCTHD, sl);
                if (rs == 1)
                {
                    ViewBag.Err = "Số lượng phải bé hơn 20 và lớn hơn 0";
                }else if (rs == 2)
                {
                    ViewBag.Err = "Số lượng tồn kho không đủ để đáp ứng";
                }
                else
                {
                    var tk = Session["TaiKhoan"] as KhachHang;
                    var hd = dao.FindHD(tk.MaKH);
                    if (hd != null)
                    {
                        Session["ListCTHD"] = dao.GetListCTHD(hd.MaHD);
                    }
                }
                return View("Index");
            }
        }
        public ActionResult ThanhToan(bool confirm)
        {
            if (confirm == true)
            {
                return RedirectToAction("Index", "ThanhToan");
            }
            return View("Index");
        }
        public ActionResult DeleteCTHD(bool confirm, int idCTHD)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                if (confirm == true)
                {
                    HoaDonDAO dao = new HoaDonDAO();
                    dao.DeleteCTHD(idCTHD);
                    var tk = Session["TaiKhoan"] as KhachHang;
                    var hd = dao.FindHD(tk.MaKH);
                    if (hd != null)
                    {
                        Session["ListCTHD"] = dao.GetListCTHD(hd.MaHD);
                    }
                }
                return View("Index");
            }
        }
    }
}