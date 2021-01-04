using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Security;

namespace WebApplication30.Models
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;
        public static void Log(String lines)
        {
            try {
                StreamWriter sw = new StreamWriter("C:\\Users\\dell5117\\Desktop\\PaypalError.log", true);
                sw.WriteLine("{0}---->{1}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), lines);
            }catch(Exception)
            {
                throw;
            }
        }
    }
}