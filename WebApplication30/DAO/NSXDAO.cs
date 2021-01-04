using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class NSXDAO
    {
        KinhContext db = null;
        public NSXDAO()
        {
            db = new KinhContext();
        }
        public List<NSX> Search(string tenNSX)
        {
            return db.NSXs.Where(i => i.TenNSX.Contains(tenNSX)&& i.flag==true).ToList();
        }
        public NSX GetNSX(int id)
        {
            return db.NSXs.Where(i => i.MaNSX == id && i.flag==true).FirstOrDefault();
        }
        public List<NSX> GetList()
        {
            return db.NSXs.Where(i => i.flag == true).ToList();
        }
        public bool DeleteNSX(int id,int idTK)
        {
            if(db.Kinhs.Where(i=>i.NSXId==id&& i.Flag == true).Count() > 0)
            {
                return false;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "XNSX";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                NSX nsx = db.NSXs.Find(id);
                log.ChiTiet = "Đã XÓA NSX tên " + nsx.TenNSX;
                nsx.flag = false;
                db.Logs.Add(log);
                db.SaveChanges();
                return true;
            }
        }
        public bool UpdateNSX(int idTK,int id, string ten,HttpPostedFileBase anh,string mota)
        {
            NSX nsx = db.NSXs.Find(id);
            if(db.NSXs.Any(i=>i.TenNSX==ten && i.MaNSX != nsx.MaNSX && i.flag==true))
            {
                return false;
            }
            else
            {
                string chiTiet = "Đã SỬA ";

                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "SNSX";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                if (nsx.TenNSX != ten)
                {
                    chiTiet = chiTiet + "'TÊN NSX' từ " + nsx.TenNSX + " sang " + ten+"; ";
                    nsx.TenNSX = ten;
                }
                if (nsx.MoTa != mota)
                {
                    chiTiet = chiTiet + "'MÔ TẢ'; ";
                    nsx.MoTa = mota;
                }

                string path = "";
                if (anh != null)
                {
                    if (anh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(anh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        anh.SaveAs(path);
                        if (nsx.Anh != fileName) {
                            nsx.Anh = fileName;
                            chiTiet = chiTiet + "'HÌNH ẢNH'; ";
                        }
                        
                    }
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
        public bool ThemNSX(int idTK,string ten, HttpPostedFileBase anh, string mota)
        {
            if (db.NSXs.Any(i => i.TenNSX == ten && i.flag==true))
            {
                return false;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "TNSX";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);
                log.ChiTiet = "Đã THÊM NSX tên "+ ten;

                NSX nsx = new NSX();
                nsx.TenNSX = ten;
                nsx.MoTa = mota;
                nsx.flag = true;

                string path = "";
                if (anh != null)
                {
                    if (anh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(anh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        anh.SaveAs(path);
                        nsx.Anh = fileName;
                    }
                    else
                    {
                        nsx.Anh = "img";
                    }
                }
                else
                {
                    nsx.Anh = "img";
                }
                db.NSXs.Add(nsx);
                db.Logs.Add(log);
                db.SaveChanges();
                return true;
            }
        }
    }
}