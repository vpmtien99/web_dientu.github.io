using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class ThuongHieuController : Controller
    {
        // GET: ThuongHieu
        public ActionResult Index()
        {
            NSXDAO dao = new NSXDAO();
            Session["Brands"] = dao.GetList();
            return View();
        }
        public ActionResult MoHinh(int id)
        {
            Session["CTKhuyenMai"] = null;
            HomeDAO dao = new HomeDAO();
            var db = new KinhContext();
            var brand = db.NSXs.Find(id);
            Session["CTBrand"] = brand;
            Session["Title"] ="Mô Hình Của "+ brand.TenNSX;
            Session["Products"] = dao.GetListTheoNSX(id);
            return RedirectToAction("Index", "MoHinhs");
        }
    }
}