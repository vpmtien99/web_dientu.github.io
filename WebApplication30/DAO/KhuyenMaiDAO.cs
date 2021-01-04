using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class KhuyenMaiDAO
    {
        KinhContext db = null;
        public KhuyenMaiDAO()
        {
            db = new KinhContext();
        }
        public List<KhuyenMai> Search(string tenkm, DateTime? batdau, DateTime? ketthuc, string phantram, DateTime? indate)
        {
            var list = db.KhuyenMais.Where(i => i.Flag == true).ToList();
            if (String.IsNullOrEmpty(tenkm) != true)
            {
                list = list.Where(i => i.TenKM.Contains(tenkm)).ToList();
            }
            if (String.IsNullOrEmpty(phantram) != true)
            {
                double temp = Convert.ToDouble(phantram);
                list = list.Where(i => i.PhanTram == temp).ToList();
            }
            if (indate != null)
            {
                list = list.Where(i => indate >= i.NgayBD && indate <= i.NgayKT).ToList();
            }
            if (batdau != null)
            {
                list = list.Where(i => i.NgayBD == batdau).ToList();
            }
            if (ketthuc != null)
            {
                list = list.Where(i => i.NgayKT == ketthuc).ToList();
            }
            return list;
        }
        public List<KhuyenMai> GetListConHan()
        {
            return db.KhuyenMais.Where(i => i.Flag == true && i.NgayBD<=DateTime.Today && DateTime.Today<=i.NgayKT).ToList();
        }
        public List<KhuyenMai> GetList()
        {
            return db.KhuyenMais.Where(i => i.Flag == true).ToList();
        }
        public KhuyenMai GetKM(int idKM)
        {
            return db.KhuyenMais.Where(i => i.Flag == true && i.MaKhuyenMai == idKM).FirstOrDefault();
        }
        public bool DeleteKM(int idTK,int idKM)
        {
            if (db.KhuyenMais.Any(i=>i.Flag==true &&i.MaKhuyenMai==idKM)==false || idKM == 7)
            {
                return false;
            }
            else
            {
                var list = db.Kinhs.Where(i => i.Flag == true && i.KhuyenMaiId == idKM && i.KhuyenMaiId != 7).ToList();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var dc = db.Kinhs.Find(item.MaKinh);
                        dc.KhuyenMaiId = 7;
                        dc.KhuyenMai = db.KhuyenMais.Find(7);
                        db.SaveChanges();
                    }
                }

                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "XKhuyenMai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                var km = db.KhuyenMais.Find(idKM);
                log.ChiTiet = "Đã XÓA chương trình khuyến mãi tên "+km.TenKM;
                db.Logs.Add(log);
                km.Flag = false;
                db.SaveChanges();
                return true;
            }
        }
        public int UpdateKM(int idTK,int idKM, string tenKM, string phanTram, DateTime ngayBD, DateTime ngayKT, HttpPostedFileBase anh)
        {
            if (db.KhuyenMais.Any(i => i.Flag == true && i.TenKM == tenKM && i.MaKhuyenMai !=idKM))
            {
                return 1;
            }
            else if (ngayBD >= ngayKT)
            {
                return 2;
            }
            else if (ngayKT <= DateTime.Now)
            {
                return 3;
            }
            else if (Convert.ToDouble(phanTram) >= 100 || Convert.ToDouble(phanTram) <= 0)
            {
                return 4;
            }
            else
            {
                string chiTiet = "Đã SỬA ";
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "SKhuyenMai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                KhuyenMai km = db.KhuyenMais.Find(idKM);
                if (km.TenKM != tenKM)
                {
                    chiTiet = chiTiet + " 'TÊN' Khuyến Mãi từ "+km.TenKM+" sang "+tenKM+"; ";
                    km.TenKM = tenKM;
                }
                if (km.PhanTram != Convert.ToDouble(phanTram))
                {
                    chiTiet = chiTiet + " 'PHẦN TRĂM' Khuyến Mãi từ " + km.PhanTram+"%" + " sang " + phanTram + "%; ";
                    km.PhanTram = Convert.ToDouble(phanTram);
                }
                if (km.NgayBD != ngayBD)
                {
                    chiTiet = chiTiet + " 'NGÀY BẮT ĐẦU' Khuyến Mãi từ " + km.NgayBD.ToShortDateString() + " sang " + ngayBD.ToShortDateString() + "; ";
                    km.NgayBD = ngayBD;
                }
                if (km.NgayKT != ngayKT)
                {
                    chiTiet = chiTiet + " 'NGÀY KẾT THÚC' Khuyến Mãi từ " + km.NgayKT.ToShortDateString() + " sang " + ngayKT.ToShortDateString() + "; ";
                    km.NgayKT = ngayKT;
                }

                string path = "";
                if (anh != null)
                {
                    if (anh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(anh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        anh.SaveAs(path);
                        if (km.AnhKM != fileName)
                        {
                            chiTiet = chiTiet + " 'HÌNH ẢNH' ";
                            km.AnhKM = fileName;
                        }
                        
                    }                 
                }
                if(chiTiet!= "Đã SỬA ")
                {
                    log.ChiTiet = chiTiet;
                    db.Logs.Add(log);
                }           
                db.SaveChanges();
                return 5;
            }
        }
    
        public int AddKM(int idTK,string tenKM,string phanTram,DateTime ngayBD,DateTime ngayKT,HttpPostedFileBase anh)
        {
            if(db.KhuyenMais.Any(i=>i.Flag==true && i.TenKM == tenKM))
            {
                return 1;
            }
            else if (ngayBD >= ngayKT)
            {
                return 2;
            }
            else if (ngayKT <= DateTime.Now)
            {
                return 3;
            }
            else if(Convert.ToDouble(phanTram)>=100 || Convert.ToDouble(phanTram) <= 0)
            {
                return 4;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "TKhuyenMai";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);
                log.ChiTiet = "Đã THÊM Chương Trình Khuyến Mãi với 'TÊN': " + tenKM + "; Thời Gian Khuyến Mãi: " + ngayBD.ToShortDateString() + "-" + ngayKT.ToShortDateString() + "; " + "Phần Trăm Khuyến Mãi: " + phanTram + "%";

                KhuyenMai km = new KhuyenMai();
                km.Flag = true;
                km.TenKM = tenKM;
                km.PhanTram = Convert.ToDouble(phanTram);
                km.NgayBD = ngayBD;
                km.NgayKT = ngayKT;

                string path = "";
                if (anh != null)
                {
                    if (anh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(anh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        anh.SaveAs(path);
                        km.AnhKM = fileName;
                    }
                    else
                    {
                        km.AnhKM = "img";
                    }
                }
                else
                {
                    km.AnhKM = "img";
                }
                db.Logs.Add(log);
                db.KhuyenMais.Add(km);
                db.SaveChanges();
                return 5;
            }
        }
    }
}