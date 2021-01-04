using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class HoaDonDAO
    {
        public static DateTime? Ngay;
        public static string TinhTrang="All";
        public static string SS="";
        public static string Tien="";
        KinhContext db = null;
        public HoaDonDAO()
        {
            db = new KinhContext();
        }
        public HoaDon GetHD(int idHD)
        {
            return db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán" && i.MaHD == idHD).FirstOrDefault();
        }
        public bool HuyDonHang(int idHD,int idTK)
        {
            var hd = db.HoaDons.Find(idHD);
            if (hd.TinhTrangDonHang == "Giao Thành Công")
            {
                return false;
            }
            foreach (var item in GetListCTHD(idHD))
            {
                var mh = db.Kinhs.Find(item.KinhId);
                mh.SoLuong = mh.SoLuong + item.SoLuong;
                db.ChiTietHoaDons.Remove(db.ChiTietHoaDons.Find(item.MaCTHD));
                db.SaveChanges();
            }
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            log.HanhDong = "HHoaDon";
            log.ChiTiet = "Đã HỦY hóa đơn với Mã: " + idHD;
            db.Logs.Add(log);
            db.HoaDons.Remove(hd);
            db.SaveChanges();
            return true;
        }
        public bool GiaoDonHang(int idHD,int idTK)
        {
            var hd = db.HoaDons.Find(idHD);
            if (hd.TinhTrangDonHang != "Chờ Giao Hàng")
            {
                return false;
            }
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            log.HanhDong = "GHoaDon";
            log.ChiTiet = "Đã Đổi trạng thái của hóa đơn với Mã: " +"<a href='#'>"+ idHD+"</a>"+" sang Đang Giao Hàng";
            db.Logs.Add(log);
            hd.TinhTrangDonHang = "Đang Giao Hàng";
            db.SaveChanges();
            return true;
        }
        public bool DaGiaoHang(int idHD, int idTK)
        {
            var hd = db.HoaDons.Find(idHD);
            if (hd.TinhTrangDonHang != "Đang Giao Hàng")
            {
                return false;
            }
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            log.HanhDong = "GHoaDon";
            log.ChiTiet = "Đã Đổi trạng thái của hóa đơn với Mã: " + "<a href='#'>" + idHD + "</a>" + " sang Giao Thành Công";
            db.Logs.Add(log);
            hd.TinhTrangDonHang = "Giao Thành Công";
            db.SaveChanges();
            return true;
        }
        public bool XacNhanDonHang(int idHD,int idTK)
        {
            var hd = db.HoaDons.Find(idHD);
            if(hd.TinhTrangDonHang!="Chưa Xác Nhận")
            {
                return false;
            }
            Log log = new Log();
            log.Ngay = DateTime.Now;
            log.KHId = idTK;
            log.KhachHang = db.KhachHangs.Find(idTK);
            log.HanhDong = "VHoaDon";
            log.ChiTiet = "Đã Xác Nhận hóa đơn với Mã: " + "<a href='#'>" + idHD + "</a>" ;
            db.Logs.Add(log);
            hd.TinhTrangDonHang = "Chờ Giao Hàng";
            db.SaveChanges();
            return true;
        }
        public List<HoaDon> Search(string mahd, DateTime? ngay, string tinhtrang, string ss, string tien)
        {
            var list = db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            if (String.IsNullOrEmpty(mahd) != true)
            {
                int ma = Convert.ToInt32(mahd);
                list = list.Where(i => i.MaHD == ma).ToList();
                return list;
            }
            if (ngay != null)
            {
                Ngay = ngay;
                list = list.Where(i => i.NgayTao.Date == ngay.Value.Date && i.NgayTao.Month==ngay.Value.Month && i.NgayTao.Year==ngay.Value.Year).ToList();
            }
            if (tinhtrang != "All")
            {
                TinhTrang = tinhtrang;
                list = list.Where(i => i.TinhTrangDonHang == tinhtrang).ToList();
            }
            if (String.IsNullOrEmpty(tien) != true)
            {
                Tien = tien;
                SS = ss;
                double tongTien = Convert.ToDouble(tien);
                if (ss == "=")
                {
                    list = list.Where(i => i.TongTien == tongTien).ToList();
                }
                else if (ss == ">")
                {
                    list = list.Where(i => i.TongTien > tongTien).ToList();
                }
                else
                {
                    list = list.Where(i => i.TongTien < tongTien).ToList();
                }
            }
            return list;
        }
        public List<HoaDon>LichSuMuaHang(int idKH)
        {
            return db.HoaDons.Where(i => i.KHId == idKH && i.TinhTrangHoaDon == "Đã Thanh Toán").OrderByDescending(i=>i.NgayTao).ToList();
        }
        public List<HoaDon> GetListHD()
        {
            return db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
        }
        public HoaDon FindHD(int idKH)
        {
            return db.HoaDons.Where(i => i.TinhTrangHoaDon == "Chưa Thanh Toán" && i.KHId == idKH).FirstOrDefault();
        }
        public double TongTien(int idHD)
        {
            double tongTien = 0;
            foreach (var item in db.ChiTietHoaDons.Where(i=>i.HDId==idHD).ToList())
            {
                tongTien = tongTien + item.ThanhTien;
            }
            var kh = db.KhachHangs.Find(db.HoaDons.Find(idHD).KHId);
            double phanTram = 0;
            foreach (var item in db.TichLuys.Where(i=>i.Flag==true).OrderByDescending(i=>i.Diem).ToList())
            {
                if (kh.DiemTichLuy >= item.Diem)
                {
                    phanTram = item.PhanTram;
                    break;
                }
            }
            tongTien= Math.Round(tongTien - (tongTien * ((phanTram * 1.0) / 100)), MidpointRounding.ToEven);
            db.HoaDons.Find(idHD).TongTien = tongTien;
            db.SaveChanges();
            return tongTien;
        }
        public int UpdateSLCTHD(int idCTHD, int sl,int slht)
        {
            if (sl <= 0 || sl > 20)
            {
                return 1;
            }

            int sltd = 0;
            int pb = 0;
            if (sl - slht < 0)
            {
                pb = 1;
                sltd = sl;
            }
            else if(sl-slht>0)
            {
                sltd = sl-slht;
            }
            else
            {
                return 9;
            }

            if (pb == 1)
            {
                var cthd = db.ChiTietHoaDons.Find(idCTHD);
                var mh = db.Kinhs.Find(cthd.KinhId);
                mh.SoLuong = mh.SoLuong + (slht-sl);
                cthd.SoLuong = sltd;
                cthd.ThanhTien = cthd.DonGia * sltd;
                db.SaveChanges();

                var hd = db.HoaDons.Find(cthd.HDId);
                TongTien(hd.MaHD);
                db.SaveChanges();

                return 3;

            }
            else
            {
                if (db.Kinhs.Find(db.ChiTietHoaDons.Find(idCTHD).KinhId).SoLuong < sltd)
                {
                    return 2;
                }
                else
                {
                    var cthd = db.ChiTietHoaDons.Find(idCTHD);
                    var mh = db.Kinhs.Find(cthd.KinhId);
                    mh.SoLuong = mh.SoLuong - sltd;
                    cthd.SoLuong = sl;
                    cthd.ThanhTien = cthd.DonGia * sl;
                    db.SaveChanges();

                    var hd = db.HoaDons.Find(cthd.HDId);
                    TongTien(hd.MaHD);
                    db.SaveChanges();

                    return 3;
                }

            }
       

        }
        public int UpdateSL(int idCTHD,int sl)
        {
            if(sl<=0 || sl > 20)
            {
                return 1;
            }
            else if (db.Kinhs.Find(db.ChiTietHoaDons.Find(idCTHD).KinhId).SoLuong < sl)
            {
                return 2;
            }
            else
            {
                var cthd = db.ChiTietHoaDons.Find(idCTHD);
                cthd.SoLuong = sl;
 
               cthd.DonGia = db.Kinhs.Find(cthd.KinhId).Gia;
                
                cthd.ThanhTien = cthd.DonGia * sl;
                db.SaveChanges();

                var hd = db.HoaDons.Find(cthd.HDId);
                hd.TongTien = TongTien(hd.MaHD);
                db.SaveChanges();

                return 3;
            }

        }
        public void DeleteCTHDUpdate(int idCTHD)
        {
            var cthd = db.ChiTietHoaDons.Find(idCTHD);
            int idHD = cthd.HDId;
            var mh = db.Kinhs.Find(cthd.KinhId);
            mh.SoLuong = mh.SoLuong + cthd.SoLuong;
            db.ChiTietHoaDons.Remove(cthd);
            db.SaveChanges();
            TongTien(idHD);
        }
        public void ThanhToan(int idHD,string ten,string diachi,string sdt)
        {
            foreach (var item in GetListCTHD(idHD))
            {
                var mh = db.Kinhs.Find(item.KinhId);
                mh.SoLuong = mh.SoLuong - item.SoLuong;
                var cthd = db.ChiTietHoaDons.Find(item.MaCTHD);
                if(mh.KhuyenMaiId!=7 && mh.KhuyenMai.NgayBD<=DateTime.Now && DateTime.Now <= mh.KhuyenMai.NgayKT)
                {
                    cthd.DonGia = Math.Round(mh.Gia - (mh.Gia * ((mh.KhuyenMai.PhanTram * 1.0) / 100)), MidpointRounding.ToEven);
                    cthd.ThanhTien = cthd.DonGia * cthd.SoLuong;
                }


                db.SaveChanges();

            }

            var hd = db.HoaDons.Find(idHD);
            hd.TenNguoiNhan = ten;
            hd.SDT = sdt;
            hd.DiaChi = diachi;
            hd.TinhTrangHoaDon = "Đã Thanh Toán";
            hd.TinhTrangDonHang = "Chưa Xác Nhận";
            hd.NgayTao = DateTime.Now;
            hd.TongTien = TongTien(idHD);
            if (hd.TongTien > 500000)
            {
                db.KhachHangs.Find(hd.KHId).DiemTichLuy += 10;
            }
            db.SaveChanges();
        }
        public void DeleteCTHD(int idCTHD)
        {
            var cthd = db.ChiTietHoaDons.Find(idCTHD);
            int idHD = cthd.HDId;
            db.ChiTietHoaDons.Remove(cthd);
            db.SaveChanges();
            TongTien(idHD);
        }
        public bool UpdateHD(int idHD, int idMH)
        {
            HoaDon hd = db.HoaDons.Find(idHD);
            foreach (var item in db.ChiTietHoaDons.Where(i => i.HDId == idHD).ToList())
            {
                if (item.KinhId == idMH)
                {
                    int sl = item.SoLuong+1;
                    if (sl > db.Kinhs.Find(idMH).SoLuong)
                    {
                        return false;
                    }
                    else
                    {
                        var cthd = db.ChiTietHoaDons.Find(item.MaCTHD);
                        cthd.SoLuong=sl;
                        cthd.DonGia = db.Kinhs.Find(idMH).Gia;
                        cthd.ThanhTien = cthd.DonGia * cthd.SoLuong;
                        db.SaveChanges();
                        hd.TongTien = TongTien(idHD);
                        db.SaveChanges();
                        return true;
                    }
                }
            }

            if (db.Kinhs.Find(idMH).SoLuong == 0)
            {
                return false;
            }
            else
            {
                ChiTietHoaDon newCthd = new ChiTietHoaDon();
                newCthd.Flag = true;
                newCthd.KinhId = idMH;
                newCthd.Kinh = db.Kinhs.Find(idMH);
                newCthd.HDId = hd.MaHD;
                newCthd.HoaDon = db.HoaDons.Find(hd.MaHD);
                newCthd.SoLuong = 1;
                    newCthd.DonGia = db.Kinhs.Find(idMH).Gia;
                newCthd.ThanhTien = newCthd.DonGia;
                db.ChiTietHoaDons.Add(newCthd);
                db.SaveChanges();
                hd.TongTien = TongTien(idHD);
                db.SaveChanges();
                return true;
            }
        }
        public List<ChiTietHoaDon>GetListCTHD(int idHD)
        {
            return db.ChiTietHoaDons.Where(i => i.HDId == idHD).ToList();
        }
        public bool CreateHD(int idKH,int idMH)
        {
            if (db.Kinhs.Find(idMH).SoLuong == 0)
            {
                return false;
            }
            else
            {

                HoaDon hd = new HoaDon();
                hd.KHId = idKH;
                hd.Flag = true;
                hd.NgayTao = DateTime.Now;
                hd.TinhTrangHoaDon = "Chưa Thanh Toán";
                hd.KhachHang = db.KhachHangs.Find(idKH);
                hd.TongTien = db.Kinhs.Find(idMH).Gia;
                db.HoaDons.Add(hd);
                db.SaveChanges();

                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.Flag = true;
                cthd.KinhId = idMH;
                cthd.Kinh = db.Kinhs.Find(idMH);
                cthd.HDId = hd.MaHD;
                    cthd.DonGia = db.Kinhs.Find(idMH).Gia;
                
                cthd.HoaDon = db.HoaDons.Find(hd.MaHD);
                cthd.SoLuong = 1;
                
                cthd.ThanhTien = cthd.DonGia;
                db.ChiTietHoaDons.Add(cthd);
                db.SaveChanges();

                return true;
            }
        }
    }
}