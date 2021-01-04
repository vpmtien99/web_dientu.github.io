using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;

namespace WebApplication30.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string username,string password)
        {
            TaiKhoanDAO dao = new TaiKhoanDAO();
            var tk = dao.DangNhap(username, password);
            if (tk != null)
            {
                Session["TaiKhoan"] = tk;
                if (tk.LoaiTK == "Admin")
                {
                    return View("../NSX/Index");
                }
                else
                {
                    return View("../Home/Index");
                }
            }
            else
            {
                ViewBag.Err="Invalid username or password";
                return View("Index");
            }
        }
    }
}