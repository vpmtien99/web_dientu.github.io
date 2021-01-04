using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication30.DAO;
using WebApplication30.Models;
using PayPal.Api;

namespace WebApplication30.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                HoaDonDAO dao = new HoaDonDAO();
                KinhContext db = new KinhContext();
                var tk = Session["TaiKhoan"] as KhachHang;
                var hd = dao.FindHD(tk.MaKH);
                if (hd != null)
                {
                    if (dao.GetListCTHD(hd.MaHD).Count == 0)
                    {
                        ViewBag.Err = "Giỏ hàng trống không thể thanh toán, hãy mua gì đó";
                        return View("../Home/Index");
                    }
                    else
                    {
                        Session["HD"] = hd;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Err = "Giỏ hàng trống không thể thanh toán, hãy mua gì đó";
                    return View("../Home/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(string tenngnhan, string diachi, string sdt,string thanhtoan)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../DangNhap/Index");
            }
            else
            {
                if (thanhtoan == "tt")
                {
                    HoaDonDAO dao = new HoaDonDAO();
                    var hd = Session["HD"] as HoaDon;
                    dao.ThanhToan(hd.MaHD, tenngnhan, diachi, sdt);
                    Session["HD"] = null;
                    Session["ListCTHD"] = null;
                    ViewBag.Err = "Thanh toán thành công";
                    
                }
                else
                {
                    return RedirectToAction("Index","Paypal",new {diaChi=diachi,SDT=sdt,ten=tenngnhan });       
                }
                return View("../Home/Index");

            }
        }

        //private Payment payment;
        //private Payment CreatePayment(APIContext apiContext,string redirectUrl)
        //{
        //    var db = new ToyContext();
        //    var listItems = new ItemList() {
        //        items=new List<Item>()
        //    };
        //    var hd = Session["HD"] as HoaDon;
        //    double tong = 0;
        //    foreach (var item in db.ChiTietHoaDons.Where(i=>i.IdHD==hd.IdHD).ToList())
        //    {
        //        tong = tong + item.ThanhTien;
        //        listItems.items.Add(new Item {
        //            name = item.DoChoi.TenDoChoi,
        //            currency = "USD",
        //            price = item.DonGia.ToString(),
        //            quantity=item.SoLuong.ToString(),
        //            sku="sku"
        //        });
        //    }
        //    double convertUSD = Math.Round((tong*1.0)/(22000*1.0),MidpointRounding.ToEven);
        //    var payer = new Payer() { payment_method="paypal"};

        //    var rediUrls = new RedirectUrls() {
        //        cancel_url = redirectUrl,
        //        return_url=redirectUrl
        //    };


        //    var details = new Details() {
        //        tax = "1",
        //        shipping = "2",
        //        subtotal = convertUSD.ToString()
        //    };


        //    var amount = new Amount() {
        //        currency = "USD",
        //        total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
        //        details=details
        //    };


        //    var transactionList = new List<Transaction>();
        //    transactionList.Add(new Transaction() {
        //        description = "TMDT Testing PayPal",
        //        invoice_number = Convert.ToString((new Random()).Next(100000)),
        //        amount = amount,
        //        item_list = listItems
        //    });


        //    payment = new Payment() {
        //        intent="sale",
        //        payer=payer,
        //        transactions=transactionList,
        //        redirect_urls=rediUrls
        //    };

        //    return payment.Create(apiContext);
        //}

        //private Payment ExecutePayment(APIContext apiContext,string payerId,string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution()
        //    {
        //        payer_id=payerId
        //    };
        //    payment = new Payment() {
        //        id=paymentId 
        //    };
        //    return payment.Execute(apiContext, paymentExecution);
        //}


        //public ActionResult PaymentWithPaypal()
        //{
        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
        //    try
        //    {
        //        string payerId = Request.Params["PayerID"];
        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "ThanhToan/PaymentWithPaypal?";
        //            var guid = Convert.ToString((new Random()).Next(100000));
        //            var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);
        //            var links = createdPayment.links.GetEnumerator();
        //            string paypalRedirectUrl = string.Empty;
        //            while (links.MoveNext())
        //            {
        //                Links link = links.Current;
        //                if (link.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    paypalRedirectUrl = link.href;
        //                }
        //            }
        //            Session.Add(guid, createdPayment.id);
        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {
        //            var guid = Request.Params["guid"];
        //            var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
        //            if (executePayment.state.ToLower() != "approved")
        //            {
        //                return View("Failure");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        PaypalLogger.Log("Error:" + ex.Message);
        //        return View("Failure");
        //    }
        //    return View("../Home/Index");
        //}
    }
}