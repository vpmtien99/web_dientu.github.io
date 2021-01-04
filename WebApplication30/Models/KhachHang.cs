using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int DiemTichLuy { get; set; }
        public string LoaiTK { get; set; }
        public bool Flag { get; set; }
        public ICollection<Log> Logs { get; set; }
        public ICollection<DanhGia> DanhGias { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}