using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication30.DAO
{
    public class StaticFuntion
    {
        public static string DisplayErr(string err)
        {
            return "<script type='text/javascript'>window.onload = function() {alert('" + err + "');</ script >";
        }
    }
}