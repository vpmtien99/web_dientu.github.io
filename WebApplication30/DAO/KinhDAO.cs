using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class KinhDAO
    {
        KinhContext db = null;
        public KinhDAO()
        {
            db = new KinhContext();
        }
        public List<Kinh> SearchPhanKM(int? mamh, string tenmh, string ss, double? tien, int loai, int nsx, int km, string trangthai)
        {
            var list = db.Kinhs.Where(i => i.Flag == true).ToList();
            
            if (mamh != null && mamh!=0)
            {
                int id = Convert.ToInt32(mamh);
                list = list.Where(i => i.MaKinh == id).ToList();
                return list;
            }
            if (String.IsNullOrEmpty(tenmh) != true)
            {
                list = list.Where(i => i.TenKinh.Contains(tenmh)).ToList();
            }
            if (tien != null & tien!=0)
            {
                double gia = Convert.ToDouble(tien);
                if (ss == "==")
                {
                    list = list.Where(i => i.Gia == gia).ToList();
                }else if (ss == ">")
                {
                    list = list.Where(i => i.Gia > gia).ToList();
                }
                else
                {
                    list = list.Where(i => i.Gia < gia).ToList();
                }
            }
            if (loai != 0)
            {
                list = list.Where(i => i.LoaiId == loai).ToList();
            }
            if (nsx != 0)
            {
                list = list.Where(i => i.NSXId == nsx).ToList();
            }
            if (km != 0)
            {
                list = list.Where(i => i.KhuyenMaiId == km).ToList();
            }
            return list;

        }
        public void BoTatCa(List<Kinh> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    var mh = db.Kinhs.Find(item.MaKinh);
                    mh.KhuyenMaiId = 7;
                    mh.KhuyenMai = db.KhuyenMais.Find(7);
                    db.SaveChanges();
                }
            }
        }
        public void Bo(int idMH)
        {
            var mh = db.Kinhs.Find(idMH);
            mh.KhuyenMaiId = 7;
            mh.KhuyenMai = db.KhuyenMais.Find(7);
            db.SaveChanges();
        }
        public void Gan(int idMH,int idKM)
        {
            var mh = db.Kinhs.Find(idMH);
            mh.KhuyenMaiId = idKM;
            mh.KhuyenMai = db.KhuyenMais.Find(idKM);
            db.SaveChanges();
        }
        public void GanTatCa(List<Kinh> list,int idKM)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    var mh = db.Kinhs.Find(item.MaKinh);
                    mh.KhuyenMaiId = idKM;
                    mh.KhuyenMai = db.KhuyenMais.Find(idKM);
                    db.SaveChanges();
                }
            }
        }
        public List<Kinh> GetListChuaKM()
        {
            return db.Kinhs.Where(i => i.Flag == true && i.KhuyenMaiId == 7 && i.KhuyenMai.NgayKT<=DateTime.Today ).ToList();
        }
        public List<Kinh> GetListDaKM(int idKM)
        {
            return db.Kinhs.Where(i => i.Flag == true && i.KhuyenMaiId != 7 && i.KhuyenMaiId == idKM).ToList();
        }
        public List<Kinh> GetList()
        {
            return db.Kinhs.Where(i => i.Flag == true).ToList();
        }
        public void GanAnh(Kinh mh,HttpPostedFileBase anhbia, HttpPostedFileBase anh1, HttpPostedFileBase anh2)
        {
            string path = "";
            if (anhbia != null)
            {
                if (anhbia.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anhbia.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anhbia.SaveAs(path);
                    mh.AnhBia = fileName;
                }
                else
                {
                    mh.AnhBia = "img";
                }
            }
            else
            {
                mh.AnhBia = "img";
            }

            string path1 = "";
            if (anh1 != null)
            {
                if (anh1.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh1.FileName);
                    path1 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh1.SaveAs(path1);
                    mh.Anh1 = fileName;
                }
                else
                {
                    mh.Anh1 = "img";
                }
            }
            else
            {
                mh.Anh1 = "img";
            }

            string path2 = "";
            if (anh2 != null)
            {
                if (anh2.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh2.FileName);
                    path2 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh2.SaveAs(path2);
                    mh.Anh2 = fileName;
                }
                else
                {
                    mh.Anh2 = "img";
                }
            }
            else
            {
                mh.Anh2 = "img";
            }
        }
        public void GanAnhUpdate(Kinh mh, HttpPostedFileBase anhbia, HttpPostedFileBase anh1, HttpPostedFileBase anh2,ref string chiTiet)
        {
            int pb = 0;
            string path = "";
            if (anhbia != null)
            {
                if (anhbia.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anhbia.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anhbia.SaveAs(path);
                    if (mh.AnhBia != fileName)
                    {
                        chiTiet = chiTiet + "'ẢNH BÌA'; ";
                        mh.AnhBia = fileName;
                    }
                    
                    
                }

            }


            string path1 = "";
            if (anh1 != null)
            {
                if (anh1.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh1.FileName);
                    path1 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh1.SaveAs(path1);
                    if (mh.Anh1 != fileName)
                    {
                        chiTiet =chiTiet+ "'ẢNH 1'; ";
                        mh.Anh1 = fileName;
                    }
                   
                }
              
            }


            string path2 = "";
            if (anh2 != null)
            {
                if (anh2.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh2.FileName);
                    path2 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh2.SaveAs(path2);
                    if (mh.Anh2 != fileName)
                    {
                        chiTiet = chiTiet + "'ẢNH 2'; ";
                        mh.Anh2 = fileName;
                    }
                }
             
            }
        }
        public Kinh GetMH(int idMH)
        {
            return db.Kinhs.Where(i => i.Flag == true && i.MaKinh == idMH).FirstOrDefault();
        }
        public int UpdateDoChoi(int idTK,int idMH,string tenmh, string gia, string sl, HttpPostedFileBase anhbia, HttpPostedFileBase anh1, HttpPostedFileBase anh2, int loai, int nsx, int km, DateTime ngay, string mota)
        {
            if (Convert.ToDouble(gia) <= 0)
            {
                return 1;
            }
            else if (Convert.ToInt32(sl) < 0)
            {
                return 2;
            }
            else
            {
                string chiTiet = "Đã SỬA Kinh có 'MÃ': "+idMH+"; ";
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "SMoHinh";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);

                Kinh mh = db.Kinhs.Find(idMH);
                if (mh.TenKinh != tenmh)
                {
                    chiTiet = chiTiet + "'TÊN' Kinh từ " + mh.TenKinh + " sang " + tenmh + "; ";
                    mh.TenKinh = tenmh;
                }
                if (mh.Gia != Convert.ToDouble(gia))
                {
                    chiTiet = chiTiet + "'GIÁ' từ " + mh.Gia + " sang " + gia + "; ";
                    mh.Gia = Convert.ToDouble(gia);
                }
                if (mh.SoLuong != Convert.ToInt32(sl))
                {
                    chiTiet = chiTiet + "'SỐ LƯỢNG' từ " + mh.SoLuong + " sang " + sl + "; ";
                    mh.SoLuong = Convert.ToInt32(sl);
                }
                if (mh.MoTa != mota)
                {
                    chiTiet = chiTiet + "'MÔ TẢ'" + "; ";
                    mh.MoTa = mota;
                }
                if (mh.NgayXB != ngay)
                {
                    chiTiet = chiTiet + "'NGÀY XUẤT BẢN' từ " + mh.NgayXB + " sang " + ngay + "; ";
                    mh.NgayXB = ngay;
                }
                if (mh.LoaiId != loai)
                {
                    chiTiet = chiTiet + "'LOẠI' Mô Hình từ " + mh.Loai.TenLoai + " sang " + db.Loais.Find(loai).TenLoai + "; ";
                    mh.LoaiId = loai;
                    mh.Loai = db.Loais.Find(loai);
                }
                if (mh.KhuyenMaiId != km)
                {
                    if (km == 7)
                    {
                        chiTiet = chiTiet + "Đã BỎ Mô Hình ra khỏi Chương Trình Khuyến Mãi tên: " + mh.KhuyenMai.TenKM + "; ";
                    }else if (mh.KhuyenMaiId == 7)
                    {
                        chiTiet = chiTiet + "Đã THÊM Mô Hình vào Chương Trình Khuyến Mãi tên: " + db.KhuyenMais.Find(km).TenKM + "; ";
                    }
                    else
                    {
                        chiTiet = chiTiet + "Chương Trình Khuyến Mãi của Mô Hình từ " + mh.KhuyenMai.TenKM + " sang " + db.KhuyenMais.Find(km).TenKM + "; ";
                    }
                    mh.KhuyenMaiId = km;
                    mh.KhuyenMai = db.KhuyenMais.Find(km);
                }
                if (mh.NSXId != nsx)
                {
                    chiTiet = chiTiet + "'NHÀ SẢN XUẤT' Mô Hình từ " + mh.NSX.TenNSX + " sang " + db.NSXs.Find(nsx).TenNSX + "; ";
                    mh.NSXId = nsx;
                    mh.NSX = db.NSXs.Find(nsx);
                }
                
                GanAnhUpdate(mh, anhbia, anh1, anh2,ref chiTiet);
                if(chiTiet!= "Đã SỬA Mô Hình có 'MÃ': " + idMH + "; ")
                {
                    log.ChiTiet = chiTiet;
                    db.Logs.Add(log);
                }
                db.SaveChanges();
                return 3;
            }
        }
        public List<Kinh> GetListTool()
        {
            return db.Kinhs.Where(i => i.Flag == true && i.Loai.TenLoai == "Dụng Cụ").Take(8).ToList();
        }
        public List<Kinh> GetListToolAll()
        {
            return db.Kinhs.Where(i => i.Flag == true && i.Loai.TenLoai == "Dụng Cụ").ToList();
        }
        public List<Kinh> Search(string tenmh, string gia, int loai, int nsx, int km, string tinhtrang)
        {
            var list = GetList();
            if (String.IsNullOrEmpty(tenmh) != true)
            {
                list = list.Where(i => i.TenKinh.Contains(tenmh)).ToList();
            }
            if (String.IsNullOrEmpty(gia) != true)
            {
                double temp = Convert.ToDouble(gia);
                list = list.Where(i => i.Gia == temp).ToList();
            }
            if (loai != 0)
            {
                list = list.Where(i => i.LoaiId == loai).ToList();
            }
            if (nsx != 0)
            {
                list = list.Where(i => i.NSXId == nsx).ToList();
            }
            if (km != 0)
            {
                list = list.Where(i => i.KhuyenMaiId == km).ToList();
            }
            if (tinhtrang == "con")
            {
                list = list.Where(i => i.SoLuong > 0).ToList();
            }
            else if(tinhtrang=="het")
            {
                list = list.Where(i => i.SoLuong == 0).ToList();
            }
            return list;
        }
        public void DeleteDoChoi(int idTK,int idMH)
        {
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.HanhDong = "XMoHinh";
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            

            Kinh mh = db.Kinhs.Find(idMH);
            log.ChiTiet = "Đã XÓA Mô Hình có 'MÃ': " + mh.MaKinh + "; 'TÊN' mô hình: " + mh.TenKinh;
            mh.Flag = false;
            db.Logs.Add(log);
            db.SaveChanges();
        }
        public int AdđoChoi(int idTK,string tenmh,string gia,string sl,HttpPostedFileBase anhbia,HttpPostedFileBase anh1,HttpPostedFileBase anh2,int loai,int nsx,int km,DateTime ngay,string mota)
        {
            if (Convert.ToDouble(gia) <= 0)
            {
                return 1;
            }else if (Convert.ToInt32(sl) < 0)
            {
                return 2;
            }
            else
            {
                Log log = new Log();
                log.Ngay = DateTime.Now;
                log.HanhDong = "TMoHinh";
                log.KHId = idTK;
                log.KhachHang = db.KhachHangs.Find(idTK);
                

                Kinh mh = new Kinh();
                mh.TenKinh = tenmh;
                mh.Gia = Convert.ToDouble(gia);
                mh.SoLuong = Convert.ToInt32(sl);
                mh.MoTa = mota;
                mh.NgayXB = ngay;
                mh.LoaiId = loai;
                mh.Loai = db.Loais.Find(loai);
                mh.KhuyenMaiId = km;
                mh.KhuyenMai = db.KhuyenMais.Find(km);
                mh.NSXId = nsx;
                mh.NSX = db.NSXs.Find(nsx);
                mh.Flag = true;
                GanAnh(mh, anhbia, anh1, anh2);
                db.Kinhs.Add(mh);
               
                db.SaveChanges();
                log.ChiTiet = "Đã THÊM Mô Hình có 'MÃ': " + mh.MaKinh + "; 'TÊN' Mô Hình: " + mh.TenKinh;
                db.Logs.Add(log);
                db.SaveChanges();
                return 3;
            }
        }
    }
}