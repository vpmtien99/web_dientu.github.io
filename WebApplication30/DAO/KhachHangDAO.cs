using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class KhachHangDAO
    {
        KinhContext db = null;
        public KhachHangDAO()
        {
            db = new KinhContext();
        }
        public KhachHang GetKH(int idKH)
        {
            return db.KhachHangs.Where(i => i.MaKH == idKH && i.Flag == true && i.LoaiTK == "Khách Hàng").FirstOrDefault();
        }
        public List<KhachHang> GetListBlock()
        {
            return db.KhachHangs.Where(i => i.Flag == false && i.LoaiTK == "Khách Hàng").ToList();
        }
        public List<KhachHang> GetList()
        {
            return db.KhachHangs.Where(i => i.Flag == true && i.LoaiTK == "Khách Hàng").ToList();
        }
        public void UpdateKH(int idTK,int id, string ho, string ten, DateTime ngaysinh, string gioitinh)
        {
            bool gioiTinh;
            if (gioitinh == "1")
            {
                gioiTinh = false;
            }
            else
            {
                gioiTinh = true;
            }
            string chiTiet = "Đã SỬA";

            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "SKhachHang";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);

            var kh = db.KhachHangs.Find(id);
            if (kh.Ho != ho)
            {
                chiTiet = chiTiet + " 'HỌ' từ " + kh.Ho + " sang " + ho + "; ";
                kh.Ho = ho;
            }
            if (kh.Ten != ten)
            {
                chiTiet = chiTiet + " 'TÊN' từ " + kh.Ten + " sang " + ten + "; ";
                kh.Ten = ten;
            }
            if (kh.NgaySinh != ngaysinh)
            {
                chiTiet = chiTiet + " 'NGÀY SINH' từ " + kh.NgaySinh.ToShortDateString() + " sang " + ngaysinh.ToShortDateString() + "; ";
                kh.NgaySinh = ngaysinh;
            }
            if (kh.GioiTinh != gioiTinh)
            {
                string old = "";
                string news = "";
                if (gioitinh == "1")
                {
                    news = "Nữ";
                }
                else
                {
                    news = "Nam";
                }
                if (kh.GioiTinh == true)
                {
                    old = "Nam";
                }
                else
                {
                    old = "Nữ";
                }
                chiTiet = chiTiet + " 'GIỚI TÍNH' từ " + old + " sang " + news + "; ";
                if (gioitinh == "1")
                {
                    kh.GioiTinh = false;
                }
                else
                {
                    kh.GioiTinh = true;
                }
            }
            if (chiTiet != "Đã SỬA")
            {
                log.ChiTiet = chiTiet;
                db.Logs.Add(log);
            }
            db.SaveChanges();

            //var kh = db.KhachHangs.Find(id);
            //kh.Ho = ho;
            //kh.Ten = ten;
            //kh.NgaySinh = ngaysinh;
            //if (gioitinh == "1")
            //{
            //    kh.GioiTinh = false;
            //}
            //else
            //{
            //    kh.GioiTinh = true;
            //}
            //db.SaveChanges();
        }
        public List<KhachHang> Search(string username, string ho, string ten,string diem,string ss)
        {
            List<KhachHang> list = db.KhachHangs.Where(i => i.Flag == true && i.LoaiTK == "Khách Hàng").ToList();
            if (String.IsNullOrEmpty(username) == false)
            {
                list = list.Where(i => i.Username.Contains(username)).ToList();
            }
            if (String.IsNullOrEmpty(ho) == false)
            {
                list = list.Where(i => i.Ho.Contains(ho)).ToList();
            }
            if (String.IsNullOrEmpty(ten) == false)
            {
                list = list.Where(i => i.Ten.Contains(ten)).ToList();
            }
            if (String.IsNullOrEmpty(diem) == false)
            {
                int temp = Convert.ToInt32(diem);
                if (ss == "=")
                {
                    list = list.Where(i => i.DiemTichLuy == temp).ToList();
                }
                else if (ss == ">")
                {
                    list = list.Where(i => i.DiemTichLuy > temp).ToList();
                }
                else
                {
                    list = list.Where(i => i.DiemTichLuy < temp).ToList();
                }
            }
            return list;
        }
        public List<KhachHang> SearchBlock(string username, string ho, string ten, string diem, string ss)
        {
            List<KhachHang> list = db.KhachHangs.Where(i => i.Flag == false && i.LoaiTK == "Khách Hàng").ToList();
            if (String.IsNullOrEmpty(username) == false)
            {
                list = list.Where(i => i.Username.Contains(username)).ToList();
            }
            if (String.IsNullOrEmpty(ho) == false)
            {
                list = list.Where(i => i.Ho.Contains(ho)).ToList();
            }
            if (String.IsNullOrEmpty(ten) == false)
            {
                list = list.Where(i => i.Ten.Contains(ten)).ToList();
            }
            if (String.IsNullOrEmpty(diem) == false)
            {
                int temp = Convert.ToInt32(diem);
                if (ss == "=")
                {
                    list = list.Where(i => i.DiemTichLuy == temp).ToList();
                }
                else if (ss == ">")
                {
                    list = list.Where(i => i.DiemTichLuy > temp).ToList();
                }
                else
                {
                    list = list.Where(i => i.DiemTichLuy < temp).ToList();
                }
            }
            return list;
        }
        public void Block(int idAdmin,int idTK)
        {
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "KKhachHang";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);

            var admin = db.KhachHangs.Find(idAdmin);
            log.ChiTiet = "Đã KHÓA tài khoản: " + admin.Username;
            db.Logs.Add(log);
            admin.Flag = false;
            db.SaveChanges();
        }
        public void UnBlock(int idAdmin, int idTK)
        {
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "MKhachHang";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);

            var admin = db.KhachHangs.Find(idAdmin);
            log.ChiTiet = "Đã MỞ KHÓA tài khoản: " + admin.Username;
            db.Logs.Add(log);
            admin.Flag = true;
            db.SaveChanges();
        }
       
    }
}