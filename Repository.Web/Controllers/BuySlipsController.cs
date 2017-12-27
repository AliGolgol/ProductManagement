using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Entry;
using Repository.ViewModel.Entry;
using System.Linq.Dynamic;
using Repository.ViewModel.Common;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class BuySlipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuySlips
        public ActionResult Index(string filter = null, int page = 1,
            int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<BuySlipHeader>();
            ViewBag.filter = filter;
            records.Content = db.BuySlips
                            .Where(x => filter == null ||
                            (x.DateCreation.Contains(filter)) ||
                            (x.DeliveryMan.Contains(filter)) ||
                            (x.Description.Contains(filter)))
                            .OrderBy(sort + " " + sortdir)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(x=>new BuySlipHeader
                            {
                                DateCreation=x.DateCreation,
                                DeliveryMan=x.DeliveryMan,
                                Description=x.Description,
                                EntrySlipType=x.EntrySlipType.Name,
                                Id=x.Id,
                                Supplier=x.Supplier.FirstName+" "+x.Supplier.LastName,
                                UserName=x.ApplicationUser.UserName
                            })
                            .ToList();

            //count
            records.TotalRecords = db.BuySlips
                                    .Where(x => filter == null ||
                                    (x.DateCreation.Contains(filter)) ||
                                    (x.DeliveryMan.Contains(filter)) ||
                                    (x.Description.Contains(filter))).Count();

            records.PageSize = pageSize;
            records.HeaderType = db.BuySlips.Select(x => x.EntrySlipType.Name).ToList();

            var buySlips = db.BuySlips.Include(b => b.Period).Include(b => b.Supplier)
                .Include(b=>b.EntrySlipType);
            return View(records);
        }

        // GET: BuySlips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlip buySlip = db.BuySlips.Find(id);
            if (buySlip == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Details", model: buySlip);
        }

        // GET: BuySlips/Create
        public ActionResult Create()
        {
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.EntryTypeId = new SelectList(db.EntrySlipTypes, "Id", "Name");
            return View();
        }

        // POST: BuySlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateCreation,Description,DeliveryMan,SupplierId,PeriodId,AppUserId")] BuySlip buySlip)
        {
            if (ModelState.IsValid)
            {
                db.BuySlips.Add(buySlip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", buySlip.PeriodId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Address", buySlip.SupplierId);
            return View(buySlip);
        }

        // GET: BuySlips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlip buySlip = db.BuySlips.Find(id);

            BuySlipViewModel bsViewModel = new BuySlipViewModel()
            {
                Id=buySlip.Id,
                DateCreation=buySlip.DateCreation,
                DeliveryMan=buySlip.DeliveryMan,
                Description=buySlip.Description,
                EntrySlipTypeId=buySlip.EntrySlipTypeId,
                SupplierId=buySlip.SupplierId,
                AppUserId=buySlip.AppUserId,
                TotalPrice = db.BuySlipItems.Where(x => x.BuySlipId == id).Sum(x => x.Price * x.Quantity),
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
            ViewBag.EntrySlipTypeId = new SelectList(db.EntrySlipTypes, "Id", "Name",buySlip.EntrySlipTypeId);
            Session["BuySlipId"] = id;
            return View(bsViewModel);
        }

        // POST: BuySlips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateCreation,Description,DeliveryMan,SupplierId,EntrySlipTypeId,AppUserId")] BuySlip buySlip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buySlip).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";
                Session["BuySlipId"] = null;
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", buySlip.SupplierId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return View(buySlip);
        }

        // GET: BuySlips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlip buySlip = db.BuySlips.Find(id);
            if (buySlip == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Delete", model: buySlip);
        }

        // POST: BuySlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuySlip buySlip = db.BuySlips.Find(id);
            bool chkSlip = db.BuySlipItems.Where(x => x.BuySlipId == id).Any();
            if (chkSlip)
            {
                TempData["message"] = "ابتدا اقلام مربوطه را حذف کنید";
                TempData["success"] = "false";
                return Json(new { success = true });
            }
            db.BuySlips.Remove(buySlip);
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
