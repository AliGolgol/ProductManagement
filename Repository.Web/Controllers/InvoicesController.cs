using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using Repository.ViewModel.Order;
using Repository.ViewModel.Common;
using System.Linq.Dynamic;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index(string filter = null, int page = 1,
            int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {

            var records = new PagedList<InvoiceHeader>();
            ViewBag.filter = filter;
            records.Content = db.Invoices
                            .Where(x => filter == null ||
                            (x.CreatedDate.Contains(filter)) ||
                            (x.Reciver.Contains(filter))||
                            (x.BillType.Name.Contains(filter)))
                            .OrderBy(sort + " " + sortdir)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(x => new InvoiceHeader
                            {
                                BillTypeName = x.BillType.Name,
                                CreatedDate = x.CreatedDate,
                                Description = x.Description,
                                Id = x.Id,
                                PaymentTypeName = x.PaymentType.Name,
                                Reciver = x.Reciver,
                                UserName = x.ApplicattionUser.UserName
                            }).ToList();
            //count
            records.TotalRecords = db.Invoices
                                    .Where(x => filter == null ||
                                    (x.CreatedDate.Contains(filter)) ||
                                    (x.Reciver.Contains(filter))).Count();
            records.PageSize = pageSize;
            records.HeaderType = db.Invoices.Select(x => x.BillType.Name).ToList();
            var invoices = db.Invoices.Include(i => i.PaymentType);
            return View(records);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Where(x => x.Id == id).Include(x => x.PaymentType).FirstOrDefault();
            InvoiceViewModel invoiceviewmodel = new InvoiceViewModel()
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
                   ProductName = x.Product.Name
               }
                   )
               .ToList(),
                TotalPrice = db.InvoiceItems.Where(i => i.InvoiceId == id).Sum(x => x.Price * x.Quantity)

            };

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Details", model: invoiceviewmodel);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,CreatedDate,Description,Reciver,PeriedId,AppUserId,PaymentTypeId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", invoice.PaymentTypeId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            InvoiceViewModel invoiceviewmodel = new InvoiceViewModel()
            {
                Id = invoice.Id,
                CreatedDate = invoice.CreatedDate,
                Description = invoice.Description,
                PaymentTypeId = invoice.PaymentTypeId,
                Reciver = invoice.Reciver,
                BillTypeId = invoice.BillTypeId,
                AppUserId=invoice.AppUserId,
                InvoiceItems = db.InvoiceItems.Where(i => i.InvoiceId == id)
               .Select(x => new InvoiceItemViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Price = x.Price,
                   PriceInQuantity = x.Price * x.Quantity,
                   ProductName = x.Product.Name,
                   Quantity = x.Quantity
               }
                   )
               .ToList(),
                TotalPrice = db.InvoiceItems.Where(i => i.InvoiceId == id).Sum(x => x.Price * x.Quantity)
            };


            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", invoice.PaymentTypeId);
            ViewBag.BillTypeId = new SelectList(db.BillTypes, "Id", "Name", invoice.BillTypeId);
            Session["InvoiceId"] = id;
            return View(invoiceviewmodel);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,CreatedDate,Description,Reciver,PeriedId,AppUserId,PaymentTypeId,BillTypeId")]
                Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";
                Session["InvoiceId"] = null;
                return RedirectToAction("Index");

            }
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", invoice.PaymentTypeId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Delete", model: invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            bool chkInvItem = db.InvoiceItems.Where(x => x.InvoiceId == id).Any();
            if (chkInvItem)
            {
                TempData["message"] = "ابتداقلام را حذف کنید";
                TempData["success"] = "false";
                return Json(new { success = true });
            }
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            TempData["message"] = "درخواست شما انجام شد";
            TempData["success"] = "true";
            return Json(new { success = true });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
