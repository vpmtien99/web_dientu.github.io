using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }
        public string BinhLuan { get; set; }
        public DateTime Ngay { get; set; }
        public bool Flag { get; set; }
        public int KHId { get; set; }
        public int KinhId { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual Kinh Kinh { get; set; }
    }
}