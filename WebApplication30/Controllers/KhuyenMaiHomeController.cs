using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class KhuyenMaiHomeController : Controller
    {
        // GET: KhuyenMaiHome
        public ActionResult Index()
        {
            KhuyenMaiDAO dao = new KhuyenMaiDAO();
            Session["KhuyenMai"] = dao.GetListConHan();
            return View();
        }
        public ActionResult MoHinh(int id)
        {
            Session["CTBrand"] = null;
            HomeDAO dao = new HomeDAO();
            var db = new KinhContext();
            var km = db.KhuyenMais.Find(id);
            Session["Title"] = "Mô Hình Của Chương Trình Khuyến Mãi: " + km.TenKM;
            Session["CTKhuyenMai"] = km;
            Session["Products"] = dao.GetListTheoKM(id);
            return RedirectToAction("Index", "MoHinhs");
        }
    }
}