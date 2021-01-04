using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;

namespace WebApplication30.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: HoaDon
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
                    HoaDonDAO dao = new HoaDonDAO();
                    if (HoaDonDAO.Ngay != null || HoaDonDAO.SS != "" || HoaDonDAO.Tien != "" || HoaDonDAO.TinhTrang != "All")
                    {
                        Session["ListHD"] = dao.Search("", HoaDonDAO.Ngay, HoaDonDAO.TinhTrang, HoaDonDAO.SS, HoaDonDAO.Tien);
                    }
                    else
                    {
                        Session["ListHD"] = dao.GetListHD();
                    }
                    return View();
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Excel(int idHD)
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
                    try
                    {
                        HoaDonDAO dao = new HoaDonDAO();
                        var list = dao.GetListCTHD(idHD);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 2;
                        int process = list.Count;

                        ws.Cells[1, 1] = "Mã";
                        ws.Cells[1, 2] = "Tên Mô Hình";
                        ws.Cells[1, 3] = "Đơn Giá";
                        ws.Cells[1, 4] = "Số Lượng";
                        ws.Cells[1, 5] = "Thành Tiền";
                        double TongTien = 0;
                        foreach (var sinhVien in list)
                        {
                            idHD = sinhVien.MaCTHD;
                            ws.Cells[index, 1] = sinhVien.KinhId;
                            ws.Cells[index, 2] = sinhVien.Kinh.TenKinh;
                            ws.Cells[index, 3] = sinhVien.Kinh.Gia;
                            ws.Cells[index, 4] = sinhVien.SoLuong;
                            ws.Cells[index, 5] = sinhVien.ThanhTien;
                            TongTien = TongTien + sinhVien.ThanhTien;
                            index += 1;
                        }
                        ws.Cells[index, 4] = "Tổng Tiền";
                        ws.Cells[index, 5] = TongTien;
                        ws.SaveAs(@path + "/HoaDonAdmin" + idHD + ".xlsx",
                        XlFileFormat.xlWorkbookDefault,
                        Type.Missing,
                        Type.Missing,
                        true, false,
                        XlSaveAsAccessMode.xlNoChange,
                        XlSaveConflictResolution.xlLocalSessionChanges,
                        Type.Missing,
                        Type.Missing);
                        excel.Quit();
                    }
                    catch (Exception ex) { }
                    ViewBag.Err = "Xuất Excel thành công";
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
                    HoaDonDAO dao = new HoaDonDAO();
                    HoaDonDAO.Ngay = null;
                    HoaDonDAO.SS = "";
                    HoaDonDAO.Tien = "";
                    HoaDonDAO.TinhTrang = "All";
                    Session["ListHD"] = dao.GetListHD();
                    return View("Index");
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        public ActionResult Update(int idHD)
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
                    var hd = dao.GetHD(idHD);
                    if (hd == null)
                    {
                        ViewBag.Err = "Không thể tìm thấy hóa đơn";
                        return View("Index");
                    }
                    else
                    {
                        return View("../CTHD/Index/?id=" + hd.MaHD);
                    }
                }
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
        [HttpPost]
        public ActionResult TimKiem(string mahd, DateTime? ngay, string tinhtrang, string ss, string tien)
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
                    Session["ListHD"] = dao.Search(mahd, ngay, tinhtrang, ss, tien);
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