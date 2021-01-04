using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public bool Flag { get; set; }
        public ICollection<Kinh> Kinhs { get; set; }
    }
}