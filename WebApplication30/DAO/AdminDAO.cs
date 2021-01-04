using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class AdminDAO
    {
        KinhContext db = null;
        public AdminDAO()
        {
            db = new KinhContext();
        }
        public KhachHang GetAdmin(int idAdmin)
        {
            return db.KhachHangs.Where(i => i.MaKH == idAdmin && i.Flag == true && i.LoaiTK == "Admin").FirstOrDefault();
        }
        public List<KhachHang> GetListBlock()
        {
            return db.KhachHangs.Where(i => i.Flag == false && i.LoaiTK == "Admin").ToList();
        }
        public List<KhachHang> GetList()
        {
            return db.KhachHangs.Where(i => i.Flag == true && i.LoaiTK == "Admin").ToList();
        }
        public void UpdateKH(int idTK ,int id,string ho,string ten,DateTime ngaysinh,string gioitinh)
        {
            bool gioiTinh;
            if (gioitinh == "1")
            {
                gioiTinh = false;
            }
            else {
                gioiTinh = true;
            }
            string chiTiet = "Đã SỬA";

            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "SAdmin";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);

            var kh = db.KhachHangs.Find(id);
            if (kh.Ho != ho)
            {
                chiTiet = chiTiet + " 'HỌ' từ " + kh.Ho + " sang " + ho+"; ";
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
            if(chiTiet!="Đã SỬA")
            {
                log.ChiTiet = chiTiet;
                db.Logs.Add(log);
            }

            


            db.SaveChanges();
        }
        public List<KhachHang> Search(string username,string ho,string ten)
        {
            List<KhachHang>list= db.KhachHangs.Where(i => i.Flag == true && i.LoaiTK == "Admin").ToList();
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
            return list;
        }
        public List<KhachHang> SearchBlock(string username, string ho, string ten)
        {
            List<KhachHang> list = db.KhachHangs.Where(i => i.Flag == false && i.LoaiTK == "Admin").ToList();
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
            return list;
        }
        public void Block(int idAdmin,int idTK)
        {
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "KAdmin";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            

            var admin = db.KhachHangs.Find(idAdmin);
            log.ChiTiet = "Đã KHÓA tài khoản " + admin.Username;
            admin.Flag = false;
            db.Logs.Add(log);
            db.SaveChanges();
        }
        public void UnBlock(int idAdmin,int idTK)
        {
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "MAdmin";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);

            var admin = db.KhachHangs.Find(idAdmin);
            log.ChiTiet = "Đã MỞ KHÓA tài khoản " + admin.Username;
            admin.Flag = true;
            db.Logs.Add(log);
            db.SaveChanges();
        }
        public bool AddKH(int idTK,string username,string ho,string ten,DateTime ngaysinh,string gioitinh)
        {
            if (db.KhachHangs.Any(i => i.Username == username))
            {
                return false;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "TAdmin";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);
                log.ChiTiet = "Đã THÊM tài khoản " + username;
                db.Logs.Add(log);

                KhachHang kh = new KhachHang();
                kh.Username = username;
                kh.Ho = ho;
                kh.Ten = ten;
                kh.NgaySinh = ngaysinh;
                kh.Password = "1";
                kh.Flag = true;
                kh.LoaiTK = "Admin";
                if (gioitinh == "1")
                {
                    kh.GioiTinh = false;
                }
                else
                {
                    kh.GioiTinh = true;
                }
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return true;
            }
        }
    }
}