using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication30.Models
{
    public class TichLuy
    {
        [Key]
        public int MaTichLuy { get; set; }
        public int Diem { get; set; }
        public double PhanTram { get; set; }
        public bool Flag { get; set; }
        
    }
}