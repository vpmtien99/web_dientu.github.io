using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class MoHinhsController : Controller
    {
        // GET: MoHinhs
        public ActionResult Index(int? page)
        {
            if (Session["Products"] != null)
            {
                var list = Session["Products"] as List<WebApplication30.Models.Kinh>;
                return View(list.ToPagedList(page ?? 1, 1));
            }
            else
            {
                Session["Title"] = "Tất Cả";
                HomeDAO dao = new HomeDAO();
                return View(dao.GetAll().ToPagedList(page ?? 1, 1));
            }
        }
        public ActionResult Reset()
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            KinhDAO dao = new KinhDAO();
            Session["Title"] = "Tất Cả";
            Session["Products"] = dao.GetList();
            return RedirectToAction("Index", "MoHinhs");
        }
        public ActionResult Search(string name,string loai,string nsx,string min,string max)
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            HomeDAO dao = new HomeDAO();
            string title;
            Session["Products"] = dao.Search(name, loai, nsx, min, max,out title);
            Session["Title"] = title;
            return RedirectToAction("Index", "MoHinhs");
        }
        public ActionResult DungCu()
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            KinhDAO dao = new KinhDAO(); 
            Session["Title"] = "Dụng Cụ Mô Hình";
            Session["Products"] = dao.GetListToolAll();
            return RedirectToAction("Index", "MoHinhs");
        }
        public ActionResult BanChay()
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            HomeDAO dao = new HomeDAO();
            Session["Title"] = "Mô Hình Bán Chạy";
            Session["Products"] = dao.GetListBanChayAll();
            return RedirectToAction("Index", "MoHinhs");
        }
        public ActionResult MHMoi()
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            HomeDAO dao = new HomeDAO();
            Session["Title"] = "Mô Hình Mới";
            Session["Products"] = dao.GetListMoiAll();
            return RedirectToAction("Index", "MoHinhs");
        }
        public ActionResult XemNhieu()
        {
            Session["CTBrand"] = null;
            Session["CTKhuyenMai"] = null;
            HomeDAO dao = new HomeDAO();
            Session["Title"] = "Mô Hình Được Xem Nhiều";
            Session["Products"] = dao.GetListXemNhieuAll();
            return RedirectToAction("Index", "MoHinhs");
        }
    }
}