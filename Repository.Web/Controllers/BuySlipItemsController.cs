using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class BuySlipItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuySlipItems
        public ActionResult Index()
        {
            var buySlipItems = db.BuySlipItems.Include(b => b.BuySlip).Include(b => b.Product).Include(b => b.Repository);
            return View(buySlipItems.ToList());
        }

        // GET: BuySlipItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlipItem buySlipItem = db.BuySlipItems.Find(id);
            if (buySlipItem == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Details", model: buySlipItem);
        }

        // GET: BuySlipItems/Create
        public ActionResult Create()
        {
            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name");
            return View();
        }
        public ActionResult CreatePop()
        {
            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name");
            BuySlipItem bsItem = new BuySlipItem();
            return PartialView(viewName: "_Create", model: bsItem);
        }
        // POST: BuySlipItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Quantity,Description,ProductId,RepositoryId")] BuySlipItem buySlipItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                GetBuySlipId();
                buySlipItem.BuySlipId = int.Parse(Session["BuySlipId"].ToString());

                DoStock(buySlipItem);
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";

                return RedirectToAction("Edit", controllerName: "BuySlips",
                                        routeValues: new { Id = int.Parse(Session["BuySlipId"].ToString()) });
            }

            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation", buySlipItem.BuySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", buySlipItem.ProductId);
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name", buySlipItem.RepositoryId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return View(buySlipItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePop([Bind(Include = "Id,Price,Quantity,Description,ProductId,RepositoryId")] BuySlipItem buySlipItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                GetBuySlipId();
                buySlipItem.BuySlipId = int.Parse(Session["BuySlipId"].ToString());

                DoStock(buySlipItem);
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";
                return Json(new { success = true });
            }

            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation", buySlipItem.BuySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", buySlipItem.ProductId);
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name", buySlipItem.RepositoryId);
            TempData["message"] = "درخواست شما انجام شد";
            TempData["success"] = "true";
            return PartialView(viewName: "_Create", model: buySlipItem);
        }

        private void DoStock(BuySlipItem buySlipItem)
        {
            db.BuySlipItems.Add(buySlipItem);
            db.SaveChanges();

            var stockItem = db.StockItems.Where(x => x.ProductId == buySlipItem.ProductId).
                FirstOrDefault();
            if (stockItem != null)
            {
                stockItem.Quantity += buySlipItem.Quantity;
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                StockItem st = new StockItem()
                {
                    ProductId = buySlipItem.ProductId,
                    Quantity = buySlipItem.Quantity
                };
                db.StockItems.Add(st);
                db.SaveChanges();
            }
        }

        private void GetBuySlipId()
        {
            PersianCalendar pc = new PersianCalendar();
            if (Session["BuySlipId"] == null)
            {
                BuySlip buyslip = new BuySlip();
                buyslip.DateCreation = string.Format("{0}/{1}/{2}",
                    pc.GetYear(DateTime.Now),
                    pc.GetMonth(DateTime.Now),
                    pc.GetDayOfMonth(DateTime.Now));
                buyslip.AppUserId = User.Identity.GetUserId<int>();
                db.BuySlips.Add(buyslip);
                db.SaveChanges();
                Session["BuySlipId"] = buyslip.Id;
            }
        }

        // GET: BuySlipItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlipItem buySlipItem = db.BuySlipItems.Find(id);
            if (buySlipItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation", buySlipItem.BuySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", buySlipItem.ProductId);
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Code", buySlipItem.RepositoryId);
            return PartialView(viewName: "Edit", model: buySlipItem);
        }

        // POST: BuySlipItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Quantity,Description,ProductId,RepositoryId,BuySlipId")] BuySlipItem buySlipItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buySlipItem).State = EntityState.Modified;

                int dif = buySlipItem.Quantity - db.BuySlipItems.
                    Where(x => x.Id == buySlipItem.Id).
                    Select(x => x.Quantity).FirstOrDefault();

                db.SaveChanges();

                var stockitem = db.StockItems.
                    FirstOrDefault(x => x.ProductId == buySlipItem.ProductId);
                stockitem.Quantity += dif;
                db.Entry(stockitem).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";
                return Json(new { success = true });
            }
            ViewBag.BuySlipId = new SelectList(db.BuySlips, "Id", "DateCreation", buySlipItem.BuySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", buySlipItem.ProductId);
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Code", buySlipItem.RepositoryId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return PartialView(viewName: "Edit", model: buySlipItem);
        }

        // GET: BuySlipItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuySlipItem buySlipItem = db.BuySlipItems.Find(id);
            if (buySlipItem == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Delete", model: buySlipItem);
        }

        // POST: BuySlipItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuySlipItem buySlipItem = db.BuySlipItems.Find(id);
            db.BuySlipItems.Remove(buySlipItem);

            var stockitem = db.StockItems.
                Where(x => x.ProductId == buySlipItem.ProductId).
                FirstOrDefault();
            stockitem.Quantity -= buySlipItem.Quantity;
            db.SaveChanges();

            db.Entry(stockitem).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = "درخواست شما انجام شد";
            TempData["success"] = "true";
            return Json(new { success = true });
        }

        public ActionResult SelectPrice(int? id)
        {
            var p = (from x in db.BuySlipItems
                    where(x.ProductId==id)
                    select x).OrderByDescending(x=>x.Price);
            var pv = p.First().Price;
            var price = db.BuySlipItems.Where(x=>x.ProductId==id).Select(x=>x.Price)
                .ToList();
            var p1 = price.Take(1);

            return Json(pv, JsonRequestBehavior.AllowGet);
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
