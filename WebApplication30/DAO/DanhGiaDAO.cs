using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication30.Models;

namespace WebApplication30.DAO
{
    public class DanhGiaDAO
    {
        KinhContext db = null;
        public DanhGiaDAO()
        {
            db = new KinhContext();
        }
        public List<DanhGia>GetList(int idMH)
        {
            return db.DanhGias.Where(i => i.Flag == true && i.KinhId == idMH).OrderByDescending(i=>i.Ngay).ToList();
        }
        public void TaoDanhGia(int idKH,int idMH,string mota)
        {
            DanhGia dg = new DanhGia();
            dg.BinhLuan = mota;
            dg.Flag = true;
            dg.Ngay = DateTime.Now;
            dg.KHId = idKH;
            dg.KinhId = idMH;
            db.DanhGias.Add(dg);
            db.SaveChanges();
        }
    }
}