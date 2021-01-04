using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class HomeDAO
    {
        KinhContext db = null;
        public HomeDAO()
        {
            db = new KinhContext();
        }
        public List<Kinh>GetListTop4(int idLoai,int idNSX,int idMH)
        {
            var list = db.Kinhs.Where(i => i.LoaiId == idLoai && i.NSXId == idNSX && i.Flag == true && i.MaKinh != idMH).OrderByDescending(i=>i.LuotXem).Take(4).ToList();
            return list;
            
        }
        public List<Kinh> GetAll()
        {
            return db.Kinhs.Where(i => i.Flag == true).ToList();
        }
        public List<Kinh> GetListXemNhieuAll()
        {
            return db.Kinhs.Where(i => i.Flag == true).OrderByDescending(i => i.LuotXem).ToList();
        }
        public List<Kinh> GetListXemNhieu()
        {
            return db.Kinhs.Where(i => i.Flag == true).OrderByDescending(i => i.LuotXem).Take(8).ToList();
        }
        public List<Kinh> GetListMoiAll()
        {
            return db.Kinhs.Where(i => i.Flag == true).OrderByDescending(i => i.NgayXB).ToList();
        }
        public List<Kinh> GetListMoi()
        {
            return db.Kinhs.Where(i => i.Flag == true).OrderByDescending(i => i.NgayXB).Take(8).ToList();
        }
        public List<Kinh> GetListBanChayAll()
        {
            HoaDonDAO dao = new HoaDonDAO();
            List<Kinh> listDC = new List<Kinh>();
            List<XemNhieuModel> list = new List<XemNhieuModel>();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            var listHD = db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listHD)
            {
                foreach (var item1 in dao.GetListCTHD(item.MaHD))
                {
                    listCTHD.Add(item1);
                }
            }


            foreach (var item in db.Kinhs.Where(i => i.Flag == true).ToList())
            {
                XemNhieuModel model = new XemNhieuModel();
                model.IDMH = item.MaKinh;
                model.SoLuong = 0;
                foreach (var item1 in listCTHD)
                {
                    if (item1.KinhId == model.IDMH)
                    {
                        model.SoLuong = model.SoLuong + item1.SoLuong;
                    }
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).ToList();
            foreach (var item in list)
            {
                listDC.Add(db.Kinhs.Find(item.IDMH));
            }
            return listDC;
        }
        public List<Kinh> Search(string name, string loai, string nsx, string min, string max,out string title)
        {
            var list = db.Kinhs.Where(i => i.Flag == true).ToList();
            title = "Tìm Kiếm Theo: ";
            if (String.IsNullOrEmpty(name) != true)
            {
                list = list.Where(i => i.TenKinh.Contains(name)).ToList();
                title = title + "+ Tên: " + name+";     ";
            }
            if (loai != "All")
            {
                
                int idLoai = Convert.ToInt32(loai);
                title = title + "+ Loại: " + db.Loais.Find(idLoai).TenLoai+";     ";
                list = list.Where(i => i.LoaiId == idLoai).ToList();
            }
            if (nsx != "All")
            {
                int idNSX = Convert.ToInt32(nsx);
                title = title + "+ Nhà Sản Xuất: " + db.NSXs.Find(idNSX).TenNSX+";     ";
                list = list.Where(i => i.NSXId == idNSX).ToList();
            }
            if (String.IsNullOrEmpty(min) != true)
            {
                title = title + "+ Min: " + min + "VNĐ ;     ";
                double tienMin = Convert.ToDouble(min);
                list = list.Where(i => i.Gia >= tienMin).ToList();
            }
            if (String.IsNullOrEmpty(max) != true)
            {
                title = title + "+ Max: " + max+"VNĐ ;     ";
                double tienMax = Convert.ToDouble(max);
                list = list.Where(i => i.Gia <= tienMax).ToList();
            }
            if(title=="Tìm Kiếm Theo: ")
            {
                title = "Tất Cả";
            }
            return list;
        }
        public List<Kinh> GetListTheoKM(int idKM)
        {            
            return db.Kinhs.Where(i => i.Flag == true && i.KhuyenMaiId == idKM).ToList();
        }
        public List<Kinh>GetListTheoNSX(int idNSX)
        {
            return db.Kinhs.Where(i => i.Flag == true && i.NSXId == idNSX).ToList();
        }
        public List<XemNhieuModel> GetListBanChay()
        {
            HoaDonDAO dao = new HoaDonDAO();
            List<XemNhieuModel> list = new List<XemNhieuModel>();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            var listHD = db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listHD)
            {
                foreach (var item1 in dao.GetListCTHD(item.MaHD))
                {
                    listCTHD.Add(item1);
                }
            }


            foreach (var item in db.Kinhs.Where(i=>i.Flag==true).ToList())
            {
                XemNhieuModel model = new XemNhieuModel();
                model.IDMH = item.MaKinh;
                model.SoLuong = 0;
                foreach (var item1 in listCTHD)
                {
                    if (item1.KinhId == model.IDMH)
                    {
                        model.SoLuong = model.SoLuong + item1.SoLuong;
                    }
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).Take(8).ToList();
            return list;
        }
    }
}