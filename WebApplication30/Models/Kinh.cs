using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class Kinh
    {
        [Key]
        public int MaKinh { get; set; }
        public double Gia { get; set; }
        public string TenKinh { get; set; }
        public int SoLuong { get; set; }
        public string AnhBia { get; set; }
        public string Anh1 { get; set; }
        public string Anh2 { get; set; }
        public long LuotXem { get; set; }
        public long LuotThich { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayXB { get; set; }
        public bool Flag { get; set; }
        public int LoaiId { get; set; }
        public int NSXId { get; set; }
        public int KhuyenMaiId { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual Loai Loai { get; set; }
        public virtual NSX NSX { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public ICollection<DanhGia> DanhGias { get; set; }
    }
}