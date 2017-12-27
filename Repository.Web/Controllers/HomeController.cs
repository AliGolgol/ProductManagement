using Microsoft.Reporting.WebForms;
using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using Repository.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using Repository.ViewModel.Order;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string filter = null, int page = 1,
            int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            #region old
            ViewBag.Title = "Home Page";
            RepositoryDataSet ds = new RepositoryDataSet();

            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            ServerReport serverReport = new ServerReport();
            var datasource = new ReportDataSource("DataSet1", db.Units);
            //serverReport.ReportServerUrl = new Uri("http://localhost:1784/Home/Index");
            //serverReport.ReportPath="/Reports/StockItems.rdlc";
            //reportViewer.ServerReport.ReportPath= "/Reports/StockItems.rdlc";
            //reportViewer.ServerReport.ReportServerUrl = new Uri("http://localhost:1784/Home/");
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) +
            //    @"Reports\Report3.rdlc";
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetStockItem",
            //    ds.Tables["StockItems"]));

            //reportViewer.LocalReport.DataSources.Clear();
            //reportViewer.LocalReport.DataSources.Add(datasource);
            //reportViewer.LocalReport.Refresh();
            //reportViewer.Width = Unit.Pixel(800);
            //reportViewer.ShowPrintButton = false;

            ViewBag.ReportViewer = null;
            #endregion

            HomeViewModel<Invoice> homeViewModel = new HomeViewModel<Invoice>()
            {
                BuyOrderCount = 0,// db.Orders.Count(),
                BuySlipCount = 0,//db.BuySlips.Count(),
                InvoiceCount = 0,//db.Invoices.Count(),
                StockCount = 0,//db.StockItems.Sum(x => x.Quantity),
                OrderCount = 12,
                Content = db.Invoices.ToList() ?? new List<Invoice>()
            };

            var records = new HomeViewModel<Invoice>();         
            ViewBag.filter = filter;
            records.Content = db.Invoices
                            .Where(x => filter == null ||
                            (x.CreatedDate.Contains(filter)) ||
                            (x.Reciver.Contains(filter)))
                            .OrderBy(sort + " " + sortdir)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize).ToList() ?? new List<Invoice>();

            records.BuyOrderCount = db.Orders.Count();
            records.BuySlipCount = db.BuySlips.Count();
            records.InvoiceCount = db.Invoices.Count();
            records.StockCount = 0;//db.StockItems.Sum(x => x.Quantity) ;
            records.OrderCount = 12;
            //count
            records.TotalRecords = db.Invoices
                                    .Where(x => filter == null ||
                                    (x.CreatedDate.Contains(filter)) ||
                                    (x.Reciver.Contains(filter))).Count();
            records.PageSize = pageSize;
            return View(records);
        }

        public JsonResult GetInvoiceItem()
        {
            var inv = db.InvoiceItems.ToList();
            var query = inv.GroupBy(i => i.Product.Name).Select(i => new InvoiceItemChartViewModel
            {
                Value = i.Key,
                Count = i.Count()
            });
            return Json(query,JsonRequestBehavior.AllowGet);
        }

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
