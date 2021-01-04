using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;

namespace WebApplication30.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Index(string email, string password, string repassword, string ho, string ten, DateTime ngaysinh, string gioitinh)
        {
            TaiKhoanDAO tk = new TaiKhoanDAO();
            int rs = tk.TaoTaiKhoan(email, password, repassword, ho, ten, ngaysinh, gioitinh);
            if (rs == 1)
            {
                ViewBag.Err = "Username đã được sử dụng";
                return View("Index");
            }
            else if (rs == 2)
            {
                ViewBag.Err = "Mật khẩu phải trùng với mật khẩu xác thực";
                return View("Index");
            }
            else
            {
                return View("../DangNhap/Index");
            }
        }
    }
}