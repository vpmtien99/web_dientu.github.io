using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class LoaiDAO
    {
        KinhContext db = null;
        public LoaiDAO()
        {
            db = new KinhContext();
        }
        public List<Loai> Search(string tenLoai)
        {
            return db.Loais.Where(i => i.TenLoai.Contains(tenLoai) && i.Flag == true).ToList();
        }
        public Loai GetLoai(int id)
        {
            return db.Loais.Where(i => i.MaLoai == id && i.Flag == true).FirstOrDefault();
        }
        public List<Loai> GetList()
        {
            return db.Loais.Where(i => i.Flag == true).ToList();
        }
        public bool DeleteLoai(int id,int idTK)
        {
            if (db.Kinhs.Where(i => i.LoaiId == id && i.Flag == true).Count() > 0)
            {
                return false;
            }
            else
            {

                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "XLoai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                Loai nsx = db.Loais.Find(id);
                log.ChiTiet = "Đã XÓA loại mô hình: " + nsx.TenLoai;
                db.Logs.Add(log);
                nsx.Flag = false;
                db.SaveChanges();
                return true;
            }
        }
        public bool UpdateLoai(int idTK,int id, string ten)
        {
            Loai nsx = db.Loais.Find(id);
            if (db.Loais.Any(i => i.TenLoai == ten && i.MaLoai != nsx.MaLoai && i.Flag == true))
            {
                return false;
            }
            else
            {
                string chiTiet = "Đã SỬA ";

                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "SLoai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);
                if (nsx.TenLoai != ten)
                {

                    chiTiet = chiTiet + "'TÊN LOẠI' từ " + nsx.TenLoai + " sang " + ten;
                    nsx.TenLoai = ten;
                }
                if(chiTiet!= "Đã SỬA ")
                {
                    log.ChiTiet = chiTiet;
                    db.Logs.Add(log);
                }
                db.SaveChanges();
                return true;

            }
        }
        public bool ThemLoai(int idTK,string ten)
        {
            if (db.Loais.Any(i => i.TenLoai == ten && i.Flag == true))
            {
                return false;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "TLoai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                Loai nsx = new Loai();
                nsx.TenLoai = ten;
                nsx.Flag = true;
                log.ChiTiet = "Đã THÊM Loại tên " + ten;
                db.Logs.Add(log);
                db.Loais.Add(nsx);
                db.SaveChanges();
                return true;
            }
        }
    }
}