using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class Log
    {
        [Key]
        public int MaLog { get; set; }
        public string HanhDong { get; set; }
        public string ChiTiet { get; set; }
        public DateTime Ngay { get; set; }
        public bool Flag { get; set; }
        public int KHId { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}