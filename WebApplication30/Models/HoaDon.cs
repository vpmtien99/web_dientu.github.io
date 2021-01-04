using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }
        public DateTime NgayTao { get; set; }
        public double TongTien { get; set; }
        public string TenNguoiNhan { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string TinhTrangDonHang { get; set; }
        public string TinhTrangHoaDon { get; set; }
        public bool Flag { get; set; }
        public int KHId { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    }
}