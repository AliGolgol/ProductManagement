using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Repository.DomainModel.Order;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Reporting.WebForms;
using Repository.DataLayer.Context;
using System.Web.UI.WebControls;
using Repository.ViewModel.Order;
using Repository.DomainModel.Entry;
using Repository.ViewModel.Entry;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Report
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            RepositoryDataSet ds = new RepositoryDataSet();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            ServerReport serverReport = new ServerReport();
            var datasource = new ReportDataSource("DataSet1", db.Units);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) +
                @"Reports\Report3.rdlc";

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(datasource);
            reportViewer.LocalReport.Refresh();
            reportViewer.Width = Unit.Pixel(700);
            reportViewer.Height = Unit.Pixel(700);
            reportViewer.ShowPrintButton = false;

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetPDFReport()
        {
            string fileName = string.Concat("Contacts.pdf");
            //string filePath = HttpContext.Current.Server.MapPath("~/Report/" + fileName);

            StockItemsController contact = new StockItemsController();
            //List<StockItem> contacList = contact.Get().ToList();

            //await Repository.Web.Reports.ReportGenerator.GeneratePDF(contacList, filePath);

            HttpResponseMessage result = null;
            //result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream("filePath", FileMode.Open));
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName;

            return result;
        }

        public ActionResult InvoiceReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice;
            InvoiceViewModel invoiceviewmodel;
            GetInvocies(id, out invoice, out invoiceviewmodel);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoiceviewmodel);
        }

        private void GetInvocies(int? id, out Invoice invoice, out InvoiceViewModel invoiceviewmodel)
        {
            invoice = db.Invoices.Find(id);
            invoiceviewmodel = new InvoiceViewModel()
            {
                Id = invoice.Id,
                CreatedDate = invoice.CreatedDate,
                Description = invoice.Description,
                PaymentTypeId = invoice.PaymentTypeId,
                Reciver = invoice.Reciver,
                PaymentTypeName = invoice.PaymentType.Name,
                InvoiceItems = db.InvoiceItems.Where(i => i.InvoiceId == id)
.Select(x => new InvoiceItemViewModel
{
    Description = x.Description,
    Price = x.Price,
    PriceInQuantity = x.Price * x.Quantity,
    ProductName = x.Product.Name,
    Quantity = x.Quantity
}
)
.ToList(),
                AboutFooter = db.Abouts.FirstOrDefault(),
                TotalPrice = db.InvoiceItems.Where(i => i.InvoiceId == id).Sum(x => x.Price * x.Quantity)

            };
        }

        public ActionResult BuySlipReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlip buySlip = db.BuySlips.Find(id);
            if (buySlip==null)
            {
                TempData["message"] = "ویرایش انجام شد";
                TempData["success"] = "false";
                return RedirectToAction(actionName:"Index",controllerName:"BuySlips");
            }
            BuySlipViewModel bsViewModel = new BuySlipViewModel()
            {

                TotalPrice = db.BuySlipItems.Where(x => x.BuySlipId == id).Sum(x => x.Price * x.Quantity),
                Id = buySlip.Id,
                DateCreation = buySlip.DateCreation,
                DeliveryMan = buySlip.DeliveryMan,
                Description = buySlip.Description,
                EntrySlipType = buySlip.EntrySlipType.Name,
                Supplier = buySlip.Supplier.FirstName + " " + buySlip.Supplier.LastName,
                UserName=buySlip.ApplicationUser.UserName,
                AboutFotter=db.Abouts.FirstOrDefault(),
                BuySlipItems = db.BuySlipItems.Where(x => x.BuySlipId == id)
                .Select(x => new BuySlipItemViewModel()
                {
                    Description = x.Description,
                    Price = x.Price,
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    PriceInQuantity = x.Price * x.Quantity,
                    Quantity = x.Quantity
                }).ToList()
            };

            if (buySlip == null)
            {
                return HttpNotFound();
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "LastName", buySlip.SupplierId);
            Session["BuySlipId"] = id;
            return View(bsViewModel);
        }

        public ActionResult DraftReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice;
            InvoiceViewModel invoiceviewmodel;
            GetInvocies(id, out invoice, out invoiceviewmodel);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoiceviewmodel);
        }
    }
}