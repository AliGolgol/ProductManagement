using Repository.DataLayer.Context;
using Repository.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.WebApi.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var about = db.Abouts.ToList();
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("~/api/Home/GetInvoiceItem")]
        public JsonResult GetInvoiceItem()
        {
            var inv = db.InvoiceItems.ToList();
            var query = inv.GroupBy(i => i.Product.Name).Select(i => new InvoiceItemChartViewModel
            {
                Value = i.Key,
                Count = i.Count()
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [Route("~/api/Home/GetBuySlipItem")]
        public JsonResult GetBuySlipItem()
        {
            var buySlipItem = db.BuySlipItems.ToList();
            var query = buySlipItem.GroupBy(i => i.Product.Name).Select(i => new InvoiceItemChartViewModel
            {
                Value = i.Key,
                Count = i.Count()
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
