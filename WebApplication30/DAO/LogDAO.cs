using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class LogDAO
    {
        KinhContext db = null;
        public LogDAO()
        {
            db = new KinhContext();
        }
        public List<Log> GetListLog(string loai)
        {
            return db.Logs.Where(i => i.HanhDong.Contains(loai)).ToList();
        }
        public List<Log> Search(string loai, string taikhoan, DateTime? ngay, string hanhdong)
        {
            var list=new List<Log>();
            if (loai != "TruyCap")
            {
                 list = db.Logs.Where(i => i.HanhDong.Contains(loai)).ToList();
            }
            else
            {
                 list = db.Logs.ToList();
            }
            if (String.IsNullOrEmpty(taikhoan) != true)
            {
                list = list.Where(i => i.KhachHang.Username == taikhoan).ToList();
            }
            if (ngay != null)
            {
                list = list.Where(i => i.Ngay.Year == ngay.Value.Year && i.Ngay.Month == ngay.Value.Month && i.Ngay.Day == ngay.Value.Day).ToList();
            }
            if (hanhdong != "All"&& String.IsNullOrEmpty(hanhdong)!=true)
            {
                list = list.Where(i => i.HanhDong.Substring(0, 1) == hanhdong).ToList();

            }
            return list;

        }
    }
}