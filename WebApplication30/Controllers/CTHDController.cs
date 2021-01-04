using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class CTHDController : Controller
    {
        // GET: CTHD
        public ActionResult Index(int id)
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
                    HoaDonDAO dao = new HoaDonDAO();
                    var hd = dao.GetHD(id);
                    if (hd == null)
                    {
                        ViewBag.Err = "Không thể tìm thấy hóa đơn";
                        return View("../HoaDon/Index");
                    }
                    else
                    {
                        Session["HoaDonAdmin"] = hd;
                        Session["ListCTHDAdmin"] = dao.GetListCTHD(id);
                        return View();
                    }
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult DeleteCTHD(bool confirm, int idCTHD)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                if (tk.LoaiTK == "Admin")
                {
                    if (confirm == true)
                    {
                        HoaDonDAO dao = new HoaDonDAO();
                        dao.DeleteCTHDUpdate(idCTHD);
                        var hd = Session["HoaDonAdmin"] as HoaDon;

                        if (hd != null)
                        {

                            Session["ListCTHDAdmin"] = dao.GetListCTHD(hd.MaHD);
                            Session["HoaDonAdmin"] = dao.GetHD(hd.MaHD);
                        }
                    }
                    return View("Index");
                }
                else
                {
                    return View("../Home/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateSL(int idCTHD, int sl, int slht)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                if (tk.LoaiTK == "Admin")
                {
                    HoaDonDAO dao = new HoaDonDAO();
                    int rs = dao.UpdateSLCTHD(idCTHD, sl, slht);
                    if (rs == 1)
                    {
                        ViewBag.Err = "Số lượng phải bé hơn 20 và lớn hơn 0";
                    }
                    else if (rs == 2)
                    {
                        ViewBag.Err = "Số lượng tồn kho không đủ để đáp ứng";
                    }
                    else if (rs == 9)
                    {
                        ViewBag.Err = "NGU";
                    }
                    else
                    {
                        var hd = Session["HoaDonAdmin"] as HoaDon;
                        if (hd != null)
                        {
                            Session["ListCTHDAdmin"] = dao.GetListCTHD(hd.MaHD);
                            Session["HoaDonAdmin"] = dao.GetHD(hd.MaHD);
                        }
                    }
                    return View("Index");
                }
                else
                {
                    return View("../Home/Index");
                }
            }
        }
        public ActionResult Delete(bool confirm, int idHD)
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
                    HoaDonDAO dao = new HoaDonDAO();
                    if (confirm == true)
                    {

                        if (dao.HuyDonHang(idHD, tk.MaKH) == false)
                        {
                            ViewBag.Err = "Không thể hủy đơn hàng";
                            return View("../HoaDon/Index");
                        }
                        else
                        {
                            ViewBag.Err = "Hủy Thành Công";
                            if (HoaDonDAO.Ngay != null || HoaDonDAO.SS != "" || HoaDonDAO.Tien != "" || HoaDonDAO.TinhTrang != "All")
                            {
                                Session["ListHD"] = dao.Search("", HoaDonDAO.Ngay, HoaDonDAO.TinhTrang, HoaDonDAO.SS, HoaDonDAO.Tien);
                            }
                            else
                            {
                                Session["ListHD"] = dao.GetListHD();
                            }
                            return View("../HoaDon/Index");
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
        public ActionResult GiaoHang(bool confirm, int idHD)
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
                    HoaDonDAO dao = new HoaDonDAO();
                    if (confirm == true)
                    {

                        if (dao.GiaoDonHang(idHD, tk.MaKH) == false)
                        {
                            ViewBag.Err = "Không thể chuyển sang trạng thái đang giao đơn hàng";
                            return View("../HoaDon/Index");
                        }
                        else
                        {
                            Session["HoaDonAdmin"] = dao.GetHD(idHD);
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
        public ActionResult DaGiao(bool confirm, int idHD)
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
                    HoaDonDAO dao = new HoaDonDAO();
                    if (confirm == true)
                    {

                        if (dao.DaGiaoHang(idHD, tk.MaKH) == false)
                        {
                            ViewBag.Err = "Không thể chuyển sang trạng thái giao hàng thành công";
                            return View("../HoaDon/Index");
                        }
                        else
                        {
                            Session["HoaDonAdmin"] = dao.GetHD(idHD);
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
        public ActionResult Check(bool confirm, int idHD)
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
                    HoaDonDAO dao = new HoaDonDAO();
                    if (confirm == true)
                    {
                       
                        if (dao.XacNhanDonHang(idHD, tk.MaKH) == false)
                        {
                            ViewBag.Err = "Không thể xác nhận đơn hàng";
                            return View("../HoaDon/Index");
                        }
                        else
                        {
                            Session["HoaDonAdmin"] = dao.GetHD(idHD);
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