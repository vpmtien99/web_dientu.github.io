using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class ChartDAO
    {
        KinhContext db = null;
        public ChartDAO()
        {
            db = new KinhContext();
        }
        public List<ChartModel> Top10Loai()
        {
            List<ChartModel> list = new List<ChartModel>();
            foreach (var item in db.Loais.Where(i => i.Flag == true).ToList())
            {
                ChartModel model = new ChartModel();
                model.ID = item.MaLoai;
                model.SoLuong = 0;
                foreach (var item1 in db.ChiTietHoaDons.Where(i => i.HoaDon.TinhTrangHoaDon == "Đã Thanh Toán" && i.Kinh.LoaiId == item.MaLoai).ToList())
                {
                    model.SoLuong = model.SoLuong + item1.SoLuong;
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).Take(10).ToList();
            return list;
        }
        public List<int> GetSLTop10Loai()
        {
            List<int> list = new List<int>();
            foreach (var item in Top10Loai())
            {
                list.Add(item.SoLuong);
            }
            return list;
        }
        public List<string> GetListTop10Loai()
        {
            List<string> list = new List<string>();
            foreach (var item in Top10Loai())
            {
                list.Add(db.Loais.Find(item.ID).TenLoai);
            }
            return list;
        }



        public List<int> ListThang()
        {
            List<int> list = new List<int>();
            for (int i = 1; i <=12 ; i++)
            {
                list.Add(i);
            }
            return list;
        }
        public List<ChartModel> Top10NSX()
        {
            List<ChartModel> list = new List<ChartModel>();
            foreach (var item in db.NSXs.Where(i => i.flag == true).ToList())
            {
                ChartModel model = new ChartModel();
                model.ID = item.MaNSX;
                model.SoLuong = 0;
                foreach (var item1 in db.ChiTietHoaDons.Where(i => i.HoaDon.TinhTrangHoaDon == "Đã Thanh Toán" && i.Kinh.NSXId == item.MaNSX).ToList())
                {
                    model.SoLuong = model.SoLuong + item1.SoLuong;
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).Take(10).ToList();
            return list;
        }
        public List<int> GetSLTop10NSX()
        {
            List<int> list = new List<int>();
            foreach (var item in Top10NSX())
            {
                list.Add(item.SoLuong);
            }
            return list;
        }
        public List<string> GetListTop10NSX()
        {
            List<string> list = new List<string>();
            foreach (var item in Top10NSX())
            {
                list.Add(db.NSXs.Find(item.ID).TenNSX);
            }
            return list;
        }
        public List<ChartModel> Top10SanPham()
        {
            List<ChartModel> list = new List<ChartModel>();
            foreach (var item in db.Kinhs.Where(i=>i.Flag==true).ToList())
            {
                ChartModel model = new ChartModel();
                model.ID = item.MaKinh;
                model.SoLuong = 0;
                foreach (var item1 in db.ChiTietHoaDons.Where(i=>i.HoaDon.TinhTrangHoaDon == "Đã Thanh Toán" && i.KinhId==item.MaKinh).ToList())
                {
                    model.SoLuong = model.SoLuong + item1.SoLuong;
                }
                list.Add(model);
            }
            list = list.OrderByDescending(i => i.SoLuong).Take(10).ToList();
            return list;
        }
        public List<int> GetSLTop10()
        {
            List<int> list = new List<int>();
            foreach (var item in Top10SanPham())
            {
                list.Add(item.SoLuong);
            }
            return list;
        }
        public List<string> GetListTop10SP()
        {
            List<string> list = new List<string>();
            foreach (var item in Top10SanPham())
            {
                list.Add(db.Kinhs.Find(item.ID).TenKinh);
            }
            return list;
        }
        public List<int> GetSLTheoNSX()
        {
            
            List<int> listSL = new List<int>();
            foreach (var item in db.NSXs.Where(i=>i.flag==true).ToList())
            {
                
                int SoLuong = db.Kinhs.Where(i => i.Flag == true && i.NSXId == item.MaNSX).Count();
                listSL.Add(SoLuong);
                
            }
            return listSL;
        }
        public List<int> GetSLTheoLoai()
        {

            List<int> listSL = new List<int>();
            foreach (var item in db.Loais.Where(i => i.Flag == true).ToList())
            {

                int SoLuong = db.Kinhs.Where(i => i.Flag == true && i.LoaiId == item.MaLoai).Count();
                listSL.Add(SoLuong);

            }
            return listSL;
        }
        public List<string> GetListLoai()
        {
            List<string> list = new List<string>();
            foreach (var item in db.Loais.Where(i => i.Flag == true).ToList())
            {
                list.Add(item.TenLoai);
            }
            return list;
        }
        public List<string> GetListNSX()
        {
            List<string> list = new List<string>();
            foreach (var item in db.NSXs.Where(i=>i.flag==true).ToList())
            {
                list.Add(item.TenNSX);
            }
            return list;
        }
        public List<int> SoLuongTheoThang()
        {
            List<int> list = new List<int>();
            for (int j = 1; j <=12 ; j++)
            {
                int soLuong = 0;
                foreach (var item in db.HoaDons.Where(i => i.TinhTrangHoaDon == "Đã Thanh Toán" && i.NgayTao.Month == j).ToList())
                {
                    foreach (var item1 in db.ChiTietHoaDons.Where(i=>i.HDId==item.MaHD).ToList())
                    {
                        soLuong = soLuong + item1.SoLuong;
                    }
                }
                list.Add(soLuong);
            }
            return list;
        }
        public List<double> DoanhThuTheoThang()
        {
            List<double> list = new List<double>();
            for (int j = 1; j <=12 ; j++)
            {
                double tongTien = 0;
                foreach (var item in db.HoaDons.Where(i=>i.TinhTrangHoaDon=="Đã Thanh Toán" && i.NgayTao.Month==j).ToList())
                {
                    tongTien = tongTien + item.TongTien;
                }
                list.Add(tongTien);
            }
            return list;
        }
    }
}