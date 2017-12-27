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

namespace Repository.Web.Controllers
{
    [Authorize]
    public class EntrySlipItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntrySlipItems
        public ActionResult Index()
        {
            var entrySlipItems = db.EntrySlipItems.Include(e => e.EntrySlip)
                .Include(e => e.Product)
                .Include(e => e.Repository);

            return View(entrySlipItems.ToList());
        }

        // GET: EntrySlipItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipItem entrySlipItem = db.EntrySlipItems.Find(id);
            if (entrySlipItem == null)
            {
                return HttpNotFound();
            }
            return View(entrySlipItem);
        }

        // GET: EntrySlipItems/Create
        public ActionResult Create()
        {
            ViewBag.EntrySlipId = new SelectList(db.EntrySlips, "Id", "CreatedDate");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.RpsId = new SelectList(db.Repositories, "Id", "Name");

            return View();
        }

        // POST: EntrySlipItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Quantity,Price,Description,RpsId,ProductId")] EntrySlipItem entrySlipItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                #region Conticue Click
                if (Request.Form["btnContinue"] != null)
                {

                    GetEntrySlipId();
                    entrySlipItem.EntrySlipId = int.Parse(Session["EntrySlipId"].ToString());
                    DoStock(entrySlipItem);

                    return RedirectToAction("Create");
                }
                #endregion

                #region Save Click
                if (Request.Form["btnSave"] != null)
                {
                    GetEntrySlipId();
                    entrySlipItem.EntrySlipId = int.Parse(Session["EntrySlipId"].ToString());
                    DoStock(entrySlipItem);
                    return RedirectToAction("Edit", "EntrySlips",
                        new { Id = int.Parse(Session["EntrySlipId"].ToString()) });
                }
                #endregion

                #region Cancel Click
                if (Request.Form["btnCancel"]!=null)
                {
                    Session["EntrySlipId"] = null;
                    return RedirectToAction("Index", "EntrySlips");
                }
                #endregion
            }

            ViewBag.EntrySlipId = new SelectList(db.EntrySlips, "Id", "CreatedDate", entrySlipItem.EntrySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", entrySlipItem.ProductId);
            return View(entrySlipItem);
        }

        private void DoStock(EntrySlipItem entrySlipItem)
        {
            db.EntrySlipItems.Add(entrySlipItem);
            db.SaveChanges();

            var stockItem = db.StockItems.Where(x => x.ProductId == entrySlipItem.ProductId).FirstOrDefault();

            if (stockItem != null)
            {
                stockItem.Quantity += entrySlipItem.Quantity;
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                StockItem st = new StockItem()
                {
                    ProductId = entrySlipItem.ProductId,
                    Quantity = entrySlipItem.Quantity
                };
                db.StockItems.Add(st);
                db.SaveChanges();
            }
        }

        private void GetEntrySlipId()
        {
            if (Session["EntrySlipId"] == null)
            {
                EntrySlip entryslip = new EntrySlip();
                db.EntrySlips.Add(entryslip);
                db.SaveChanges();
                Session["EntrySlipId"] = entryslip.Id;
            }
        }

        // GET: EntrySlipItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipItem entrySlipItem = db.EntrySlipItems.Find(id);
            if (entrySlipItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntrySlipId = new SelectList(db.EntrySlips, "Id", "Name", entrySlipItem.EntrySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", entrySlipItem.ProductId);
            return View(entrySlipItem);
        }

        // POST: EntrySlipItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,Price,Description,EntrySlipId,RpsId,ProductId")] EntrySlipItem entrySlipItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrySlipItem).State = EntityState.Modified;

                int dif = entrySlipItem.Quantity - db.EntrySlipItems.
                    Where(x => x.Id == entrySlipItem.Id)
                    .Select(x => x.Quantity).FirstOrDefault();

                db.SaveChanges();

                var stockitem = db.StockItems.
                    FirstOrDefault(x => x.ProductId == entrySlipItem.ProductId);
                stockitem.Quantity += dif;
                db.Entry(stockitem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.EntrySlipId = new SelectList(db.EntrySlips, "Id", "Name", entrySlipItem.EntrySlipId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", entrySlipItem.ProductId);
            return View(entrySlipItem);
        }

        // GET: EntrySlipItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipItem entrySlipItem = db.EntrySlipItems.Find(id);
            if (entrySlipItem == null)
            {
                return HttpNotFound();
            }
            return View(entrySlipItem);
        }

        // POST: EntrySlipItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntrySlipItem entrySlipItem = db.EntrySlipItems.Find(id);
            db.EntrySlipItems.Remove(entrySlipItem);

            var stockitem = db.StockItems.Where(x => x.ProductId == entrySlipItem.ProductId)
                .FirstOrDefault();
            stockitem.Quantity -= entrySlipItem.Quantity;
            db.SaveChanges();

            db.Entry(stockitem).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
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
