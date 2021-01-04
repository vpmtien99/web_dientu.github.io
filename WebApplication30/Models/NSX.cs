using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class NSX
    {
        [Key]
        public int MaNSX { get; set; }
        public string TenNSX { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public bool flag { get; set; }
        public ICollection<Kinh> Kinhs { get; set; }

    }
}