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
    public class ChiTietMHController : Controller
    {
        // GET: ChiTietKinh
        public ActionResult Index(int id,int?page)
        {
            KinhContext db = new KinhContext();
            Kinh mh = db.Kinhs.Find(id);
            mh.LuotXem++;
            db.SaveChanges();
            DanhGiaDAO dao = new DanhGiaDAO();
            Session["CTMoHinh"] = mh;
            if (page != null)
            {
                ViewBag.Roll = "roll";
            }
            return View(dao.GetList(mh.MaKinh).ToPagedList(page ?? 1, 7));
        }
        [HttpPost]
        public ActionResult DanhGiaMH(int id,string danhgia)
        {
            if (Session["TaiKhoan"] != null)
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                DanhGiaDAO dao = new DanhGiaDAO();
                dao.TaoDanhGia(tk.MaKH, id, danhgia);
                ViewBag.Roll = "roll";
                return View("Index", dao.GetList(id).ToPagedList(1, 7));
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult MuaNgay(int idMH) {
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
        public ActionResult ThemGioHang(int idMH)
        {
            HoaDonDAO dao = new HoaDonDAO();
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.Roll = null;
                var tk = Session["TaiKhoan"] as KhachHang;
                var hd = dao.FindHD(tk.MaKH);
                if (hd == null)
                {
                    if(dao.CreateHD(tk.MaKH, idMH) == false)
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
                DanhGiaDAO daoDG = new DanhGiaDAO();
                
                return View("Index",daoDG.GetList(idMH).ToPagedList(1,7));
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
    }
}