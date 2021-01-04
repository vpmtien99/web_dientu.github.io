using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class TaiKhoanDAO
    {
        KinhContext db = null;
        public TaiKhoanDAO() {
            db = new KinhContext();
        }
        public int UpdateTaiKhoan(int id, string email, string password, string newpassword, string repassword, string ho, string ten, DateTime ngaysinh, int gioitinh)
        {
            if(db.KhachHangs.Any(i=>i.Username==email && i.Password == password) == false)
            {
                return 1;
            }
            else
            {
                if(String.IsNullOrEmpty(newpassword)!=true || String.IsNullOrEmpty(repassword) != true)
                {
                    if (newpassword == repassword)
                    {
                        var tk = db.KhachHangs.Find(id);
                        tk.Password = newpassword;
                        tk.Ho = ho;
                        tk.Ten = ten;
                        tk.NgaySinh = ngaysinh;
                        if (gioitinh == 0)
                        {
                            tk.GioiTinh = true;
                        }
                        else
                        {
                            tk.GioiTinh = false;
                        }
                        db.SaveChanges();
                        return 3;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    var tk = db.KhachHangs.Find(id);
                    tk.Ho = ho;
                    tk.Ten = ten;
                    tk.NgaySinh = ngaysinh;
                    if (gioitinh == 0)
                    {
                        tk.GioiTinh = true;
                    }
                    else
                    {
                        tk.GioiTinh = false;
                    }
                    db.SaveChanges();
                    return 4;
                }
            }
        }
        public KhachHang DangNhap(string username,string password)
        {
            return db.KhachHangs.Where(i => i.Username == username && i.Password == password && i.Flag == true).FirstOrDefault();
        }
        public int TaoTaiKhoan(string email,string password,string repassword,string ho,string ten,DateTime ngaysinh,string gioitinh)
        {
            if (db.KhachHangs.Any(i => i.Username == email) == true)
            {
                return 1;
            }
            else if (password != repassword)
            {
                return 2;
            }
            else
            {
                KhachHang kh = new KhachHang();
                kh.Ho = ho;
                kh.Ten = ten;
                kh.NgaySinh = ngaysinh;
                if (gioitinh == "1")
                {
                    kh.GioiTinh = false;
                }
                else
                {
                    kh.GioiTinh = true;
                }
                kh.DiemTichLuy = 0;
                kh.LoaiTK = "Khách Hàng";
                kh.Username = email;
                kh.Password = password;
                kh.Flag = true;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return 3;
            }
        }

    }
}