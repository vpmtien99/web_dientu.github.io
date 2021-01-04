using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class KhuyenMai
    {
        [Key]
        public int MaKhuyenMai { get; set; }
        public string TenKM { get; set; }
        public double PhanTram { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        public string AnhKM { get; set; }
        public bool Flag { get; set; }
        public ICollection<Kinh> Kinhs { get; set; }
    }
}