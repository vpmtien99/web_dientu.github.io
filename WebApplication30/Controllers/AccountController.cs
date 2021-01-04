using Microsoft.Office.Interop.Excel;
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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Excel(int id)
        {
            try
            {
                HoaDonDAO dao = new HoaDonDAO();
                var list = dao.GetListCTHD(id);
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
                    id = sinhVien.MaCTHD;
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
                ws.SaveAs(@path + "/HoaDonAdmin" + id + ".xlsx",
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


            return RedirectToAction("LichSuMuaHang", "Account");
        }
        public ActionResult ChiTietLS(int id)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                HoaDonDAO dao = new HoaDonDAO();
                Session["HDHome"] = dao.GetHD(id);
                Session["CTLS"] = dao.GetListCTHD(id);
                return View();
            }
        }
        public ActionResult LichSuMuaHang(int? page)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                HoaDonDAO dao = new HoaDonDAO();
                return View(dao.LichSuMuaHang(tk.MaKH).ToPagedList(page??1,10));
            }
        }
        [HttpPost]
        public ActionResult Index(int id,string email,string password,string newpassword,string repassword,string ho,string ten,DateTime ngaysinh,int gioitinh)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                TaiKhoanDAO dao = new TaiKhoanDAO();
                int rs=dao.UpdateTaiKhoan(id, email, password, newpassword, repassword, ho, ten, ngaysinh, gioitinh);
                if (rs == 1) {
                    ViewBag.Err = "Sai mật khẩu";
                }else if (rs == 2)
                {
                    ViewBag.Err = "Mật khẩu mới và mật khẩu xác thực phải bằng nhau";
                }
                else
                {
                    ViewBag.Err = "Cập nhật thành công";
                    KhachHang tk  ;
                    if (rs == 3)
                    {
                         tk = dao.DangNhap(email, newpassword);
                    }
                    else
                    {
                         tk = dao.DangNhap(email, password);
                    }
                   
                    if (tk == null)
                    {
                        Session["TaiKhoan"] = null;
                        return View("../DangNhap/Index");
                    }
                    else
                    {
                        Session["TaiKhoan"] = tk;
                        
                    }
                   
                }
                return View("Index");
            }
        }
    }
}