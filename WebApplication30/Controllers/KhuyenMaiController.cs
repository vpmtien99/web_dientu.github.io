using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class KhuyenMaiController : Controller
    {
        public ActionResult PhanKM()
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
                    KinhDAO dao = new KinhDAO();
                    Session["ListCKM"] = dao.GetListChuaKM();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult TimKiemPhanKM(int? mamh, string tenmh, string ss, double? tien, int loai, int nsx, int km, string trangthai)
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
                    KinhDAO dao = new KinhDAO();
                    TimKiemKMModel model = new TimKiemKMModel();
                    if (mamh != null)
                    {
                        model.MaMH = Convert.ToInt32(mamh);
                    }
                    if (String.IsNullOrEmpty(tenmh) == true)
                    {
                        model.TenMH = "";
                    }
                    else
                    {
                        model.TenMH = tenmh;
                    }
                    model.SoSanh = ss;
                    if (tien != null)
                    {
                        model.GiaMH = Convert.ToDouble(tien);
                    }
                    model.Loai = loai;
                    model.NSX = nsx;
                    model.KhuyenMai = km;
                    model.TrangThai = trangthai;
                    Session["TimKiemKM"] = model;
                    if (trangthai == "da")
                    {
                        Session["IDKM"] = null;
                        Session["ListDKM"] = dao.SearchPhanKM(mamh, tenmh, ss, tien, loai, nsx, km, trangthai);
                    }
                    else
                    {
                        Session["ListCKM"] = dao.SearchPhanKM(mamh, tenmh, ss, tien, loai, nsx, km, trangthai);
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult BoTatCa(bool confirm)
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
                        if (Session["ListDKM"] == null)
                        {
                            ViewBag.Err = "Chọn chượng trình khuyến mãi để thao tác";
                        }
                        else
                        {
                            KinhDAO dao = new KinhDAO();
                            var list = Session["ListDKM"] as List<WebApplication30.Models.Kinh>;
                            dao.BoTatCa(list);
                            Session["ListDKM"] = null;
                            if (Session["TimKiemKM"] != null)
                            {
                                var timKiem = Session["TimKiemKM"] as TimKiemKMModel;

                                Session["ListCKM"] = dao.SearchPhanKM(timKiem.MaMH, timKiem.TenMH, timKiem.SoSanh, timKiem.GiaMH, timKiem.Loai, timKiem.NSX, timKiem.KhuyenMai, timKiem.TrangThai);
                            }
                            else
                            {
                                ResetPhanKM();
                            }
                        }
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult GanTatCa(bool confirm)
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
                        if (Session["ListCKM"] == null)
                        {
                            ViewBag.Alert = "Cần có danh sách để gán khuyến mãi";
                        }
                        else if (Session["IDKM"] == null)
                        {
                            ViewBag.Err = "Chọn chượng trình khuyến mãi để gán";
                        }
                        else
                        {
                            KinhDAO dao = new KinhDAO();
                            var list = Session["ListCKM"] as List<WebApplication30.Models.Kinh>;
                            var id = Session["IDKM"] as string;
                            dao.GanTatCa(list, Convert.ToInt32(id));
                            Session["ListCKM"] = null;
                            Session["ListDKM"] = dao.GetListDaKM(Convert.ToInt32(id));

                        }
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Bo(int idMH)
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
                    var id = Session["IDKM"] as string;
                    KinhDAO dao = new KinhDAO();
                    dao.Bo(idMH);
                    Session["ListDKM"] = dao.GetListDaKM(Convert.ToInt32(id));
                    if (Session["TimKiemKM"] != null)
                    {
                        var timKiem = Session["TimKiemKM"] as TimKiemKMModel;

                        Session["ListCKM"] = dao.SearchPhanKM(timKiem.MaMH, timKiem.TenMH, timKiem.SoSanh, timKiem.GiaMH, timKiem.Loai, timKiem.NSX, timKiem.KhuyenMai, timKiem.TrangThai);
                    }
                    else
                    {
                        ResetPhanKM();
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Gan(int idMH)
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
                    if (Session["IDKM"] == null)
                    {
                        ViewBag.Err = "Chọn chượng trình khuyến mãi để gán";
                    }
                    else
                    {
                        var id = Session["IDKM"] as string;
                        KinhDAO dao = new KinhDAO();
                        dao.Gan(idMH, Convert.ToInt32(id));
                        Session["ListDKM"] = dao.GetListDaKM(Convert.ToInt32(id));
                        if (Session["TimKiemKM"] != null)
                        {
                            var timKiem = Session["TimKiemKM"] as TimKiemKMModel;

                            Session["ListCKM"] = dao.SearchPhanKM(timKiem.MaMH, timKiem.TenMH, timKiem.SoSanh, timKiem.GiaMH, timKiem.Loai, timKiem.NSX, timKiem.KhuyenMai, timKiem.TrangThai);
                        }
                        else
                        {
                            ResetPhanKM();
                        }
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        // GET: KhuyenMai
        [HttpPost]
        public ActionResult ListKhuyenMai(string idkm)
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
                    KinhDAO dao = new KinhDAO();

                    if (idkm == "Trống")
                    {
                        Session["IDKM"] = null;
                        Session["ListDKM"] = null;
                    }
                    else
                    {
                        Session["IDKM"] = idkm;
                        Session["ListDKM"] = dao.GetListDaKM(Convert.ToInt32(idkm));
                    }
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult ResetPhanKM()
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
                    KinhDAO dao = new KinhDAO();
                    Session["TimKiemKM"] = null;
                    Session["ListCKM"] = dao.GetListChuaKM();
                    return View("PhanKM");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
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
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    Session["ListKM"] = dao.GetList();
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idKM)
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
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    var km = dao.GetKM(idKM);
                    if (km == null)
                    {
                        ViewBag.Alert = "Không thể tìm thấy chương trình khuyến mãi";
                    }
                    else
                    {
                        Session["UpdateKM"] = km;
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
        public ActionResult TimKiem(string tenkm, DateTime? batdau, DateTime? ketthuc, string phantram, DateTime? indate)
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
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    Session["ListKM"] = dao.Search(tenkm, batdau, ketthuc, phantram, indate);
                    return View("Index");
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
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    Session["ListKM"] = dao.GetList();
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Delete(bool confirm, int idKM)
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
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    if (confirm == true)
                    {

                        if (dao.DeleteKM(tk.MaKH, idKM) == false)
                        {
                            ViewBag.Alert = "Không thể xóa chương trình khuyến mãi này";
                        }
                        else
                        {
                            if (Session["UpdateKM"] != null)
                            {
                                var km = Session["UpdateKM"] as KhuyenMai;
                                if (km.MaKhuyenMai == idKM)
                                {
                                    Session["UpdateKM"] = null;
                                }
                            }
                            Session["ListKM"] = dao.GetList();
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
        public ActionResult Index(string tenkm, DateTime batdau, DateTime ketthuc, string phantram, HttpPostedFileBase anh)
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
                   
                    KhuyenMaiDAO dao = new KhuyenMaiDAO();
                    if (Session["UpdateKM"] == null)
                    {
                        int rs = dao.AddKM(tk.MaKH, tenkm, phantram, batdau, ketthuc, anh);
                        if (rs == 1)
                        {
                            ViewBag.Ten = "Tên khuyến mãi đã tồn tại";
                        }
                        else if (rs == 2)
                        {
                            ViewBag.BD = "Ngày bắt đầu phải bé hơn ngày kết thúc";
                        }
                        else if (rs == 3)
                        {
                            ViewBag.KT = "Ngày kết thúc phải lớn hơn ngày hiện tại";
                        }
                        else if (rs == 4)
                        {
                            ViewBag.PT = "Phần trăm phải lớn hơn 0 và bé hơn 100";
                        }
                        else
                        {
                            Session["ListKM"] = dao.GetList();
                        }
                    }
                    else
                    {
                        var km = Session["UpdateKM"] as KhuyenMai;
                        int rs = dao.UpdateKM(tk.MaKH, km.MaKhuyenMai, tenkm, phantram, batdau, ketthuc, anh);
                        if (rs == 1)
                        {
                            ViewBag.Ten = "Tên khuyến mãi đã tồn tại";
                        }
                        else if (rs == 2)
                        {
                            ViewBag.BD = "Ngày bắt đầu phải bé hơn ngày kết thúc";
                        }
                        else if (rs == 3)
                        {
                            ViewBag.KT = "Ngày kết thúc phải lớn hơn ngày hiện tại";
                        }
                        else if (rs == 4)
                        {
                            ViewBag.PT = "Phần trăm phải lớn hơn 0 và bé hơn 100";
                        }
                        else
                        {
                            Session["UpdateKM"] = null;
                            Session["ListKM"] = dao.GetList();
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