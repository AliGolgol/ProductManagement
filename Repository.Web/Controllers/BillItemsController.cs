using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Catalog;
using Repository.DomainModel.Order;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class BillItemsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillItems
        public ActionResult Index()
        {
            var billItems = db.BillItems.Include(b => b.Bill).Include(b => b.Product);
            return View(billItems.ToList());
        }

        // GET: BillItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return HttpNotFound();
            }
            return View(billItem);
        }

        // GET: BillItems/Create
        public ActionResult Create(int Id)
        {
            ViewBag.BillId = Id;
            BillItem b = new BillItem()
            {
                BillId=Id,
                Description="test"
            };
            //Success(string.Format("خبر با عنوان  <b>{0}</b> با موفقیت ذخیره گردید!",Id), true);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");

            return View(b);
        }

        // POST: BillItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Quantity,Price,ProductId,BillId")] BillItem billItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var stockitem = db.StockItems.
                           Where(x => x.ProductId == billItem.ProductId)
                           .FirstOrDefault();

                int dif = stockitem.Quantity - billItem.Quantity;


                #region Continue Click

                if (Request.Form["btnContinue"] != null)
                {
                    var billType = db.Bills.Include(b=>b.BillType)
                        .Where(x=>x.Id==billItem.BillId)
                        .Select(p => p.BillType.IsRemoval).FirstOrDefault();

                    if (stockitem==null  ||stockitem.Quantity==0)
                    {
                        TempData["message"] = string.Format("کالای با نام  {0}  موجود نمی باشد", stockitem.Products.Name);
                        return RedirectToAction("Create",
                                new { Id = billItem.BillId });
                    }

                    if (billType)
                    {
                        if (dif > 0)
                        {
                            db.BillItems.Add(billItem);
                            db.SaveChanges();

                            DoSctockItem(billItem, stockitem);
                            TempData["message"] = "رکورد مورد نظر ثبت گردید";
                            return RedirectToAction("Create",
                                new {Id=billItem.BillId});
                        }
                        else
                        {
                            //return message:"It can possible to order this product
                            //becuase of quantity
                            TempData["message"] = string.Format("آیتم به شماره  {0}  ثبت نگردید موجودی منفی می شود", billItem.BillId);
                            return RedirectToAction("Create",
                                 new { Id = billItem.BillId });
                        }

                    }
                    else
                    {
                        TempData["message"] = string.Format("آیتم به شماره  {0}  ثبت گردیده برو روی موجودی تائیر ندارد", billItem.BillId);
                        db.BillItems.Add(billItem);
                        db.SaveChanges();
                        return RedirectToAction("Create",
                                new { Id = billItem.BillId });
                    }
                }

                #endregion

                #region Save Click
                if (Request.Form["btnSave"] != null)
                {
                    var billType = db.Bills.Include(b => b.BillType)
                       .Where(x => x.Id == billItem.BillId)
                       .Select(p => p.BillType.IsRemoval).FirstOrDefault();

                    if (stockitem == null || stockitem.Quantity == 0)
                    {
                        TempData["message"] = string.Format("کالای با نام  {0}  موجود نمی باشد", stockitem.Products.Name);
                        return RedirectToAction("Index","Bills");
                    }

                    if (billType)
                    {
                        if (dif > 0)
                        {
                            db.BillItems.Add(billItem);
                            db.SaveChanges();

                            DoSctockItem(billItem, stockitem);
                            TempData["message"] = "رکورد مورد نظر ثبت گردید";
                            TempData["success"] = true;
                            return RedirectToAction("Index", "Bills");
                        }
                        else
                        {
                            //return message:"It can possible to order this product
                            //becuase of quantity
                            TempData["message"] = string.Format("آیتم به شماره  {0}  ثبت نگردید موجودی منفی می شود", billItem.BillId);
                            TempData["success"] = false;
                            return RedirectToAction("Index", "Bills");
                        }

                    }
                    else
                    {
                        TempData["message"] = string.Format("آیتم به شماره  {0}  ثبت گردیده برو روی موجودی تائیر ندارد", billItem.BillId);
                        TempData["success"] = false;
                        db.BillItems.Add(billItem);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Bills");
                    }
                }
                #endregion
                
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", billItem.ProductId);
            return View(billItem);
        }

        // GET: BillItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                TempData["message"] = "مورد یافت نشد";
                TempData["success"] = false;
                return RedirectToAction("Index", "Bills");
            }
            ViewBag.BillId = new SelectList(db.Bills, "Id", "CreatedDate", billItem.BillId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", billItem.ProductId);
            return View(billItem);
        }

        // POST: BillItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Quantity,Price,ProductId,BillId")] BillItem billItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillId = new SelectList(db.Bills, "Id", "CreatedDate", billItem.BillId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", billItem.ProductId);
            return View(billItem);
        }

        // GET: BillItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return HttpNotFound();
            }
            return View(billItem);
        }

        // POST: BillItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillItem billItem = db.BillItems.Find(id);
            db.BillItems.Remove(billItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void DoSctockItem(BillItem billItem, StockItem stockitem)
        {
            stockitem.Quantity -= billItem.Quantity;
            db.Entry(stockitem).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void GetInvoiceId()
        {
            if (Session["InvoiceId"] == null)
            {
                Invoice invoice = new Invoice();
                db.Invoices.Add(invoice);
                db.SaveChanges();
                Session["InvoiceId"] = invoice.Id;
            }
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
