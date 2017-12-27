using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using System.Globalization;
using System;
using Repository.ViewModel.Order;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class InvoiceItemsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: InvoiceItems
        public ActionResult Index()
        {
            var invoiceItems = db.InvoiceItems.Include(i => i.Invoice).Include(i => i.Product);
            return View(invoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Details", model: invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name");
            
            return View();
        }
        public ActionResult CreatePop()
        {
            InvoiceItem invItm = new InvoiceItem();
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name");
            return PartialView(viewName: "_Create", model: invItm);
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Quantity,Price,ProductId,InvoiceId,RepositoryId")] InvoiceItem invoiceItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var stockitem = db.StockItems.
                           Where(x => x.ProductId == invoiceItem.ProductId)
                           .FirstOrDefault();

                int dif = stockitem.Quantity - invoiceItem.Quantity;

                #region Continue Click

                if (dif > 0)
                {

                    GetInvoiceId();
                    int invId = int.Parse(Session["InvoiceId"].ToString());
                    invoiceItem.InvoiceId = invId;


                    var invoiceItemExist = db.InvoiceItems
                        .Where(x => x.InvoiceId == invId
                        && x.ProductId == invoiceItem.ProductId).FirstOrDefault();

                    if (invoiceItemExist != null)
                    {
                        //update invoice item
                        invoiceItemExist.Quantity += invoiceItem.Quantity;

                        //update stock item
                        DoSctockItem(invoiceItem, stockitem);
                        TempData["message"] = "درخواست شما انجام شد";
                        TempData["success"] = "true";
                        return RedirectToAction("Edit", "Invoices", new { Id = int.Parse(Session["InvoiceId"].ToString()) });
                    }
                    else
                    {
                        db.InvoiceItems.Add(invoiceItem);
                        db.SaveChanges();

                        DoSctockItem(invoiceItem, stockitem);
                        TempData["message"] = "انجام شد";
                        TempData["success"] = "true";

                        return RedirectToAction("Edit", "Invoices", new { Id = int.Parse(Session["InvoiceId"].ToString()) });
                    }

                }
                else
                {
                    //return message:"It can possible to order this product
                    //becuase of quantity
                    TempData["message"] = "این مورد به دلیل منفی شدن موجودی امکان ندارد";
                    TempData["success"] = "false";
                    return RedirectToAction("Create");
                }
                #endregion

            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", invoiceItem.ProductId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return View(invoiceItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePop([Bind(Include = "Id,Description,Quantity,Price,ProductId,InvoiceId,RepositoryId")] InvoiceItem invoiceItem,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var stockitem = db.StockItems.
                           Where(x => x.ProductId == invoiceItem.ProductId)
                           .FirstOrDefault();

                int dif = stockitem.Quantity - invoiceItem.Quantity;

                //if (dif > 0)
                //{
                    GetInvoiceId();
                    int invId = int.Parse(Session["InvoiceId"].ToString());
                    invoiceItem.InvoiceId = invId;


                    var invoiceItemExist = db.InvoiceItems
                        .Where(x => x.InvoiceId == invId
                        && x.ProductId == invoiceItem.ProductId).FirstOrDefault();

                    if (invoiceItemExist != null)
                    {
                        //update invoice item
                        invoiceItemExist.Quantity += invoiceItem.Quantity;

                        //update stock item
                        DoSctockItem(invoiceItem, stockitem);
                        TempData["message"] = "درخواست شما انجام شد";
                        TempData["success"] = "true";
                        return Json(new { success = true });
                    }
                    else
                    {
                        db.InvoiceItems.Add(invoiceItem);
                        db.SaveChanges();

                        DoSctockItem(invoiceItem, stockitem);
                        TempData["message"] = "انجام شد";
                        TempData["success"] = "true";

                        return Json(new { success = true });
                    }

                //}
                //else
                //{
                //    TempData["message"] = "این مورد به دلیل منفی شدن موجودی امکان ندارد";
                //    TempData["success"] = "false";
                //    return PartialView(viewName: "_Create", model: invoiceItem);
                //}
            }
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", invoiceItem.ProductId);
            return PartialView(viewName: "_Create", model: invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "CreatedDate", invoiceItem.InvoiceId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", invoiceItem.ProductId);
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name", invoiceItem.RepositoryId);
            Session["quantity"] = invoiceItem.Quantity;
            Session["productId"] = invoiceItem.ProductId;
            return PartialView(viewName: "Edit", model: invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Description,Quantity,Price,ProductId,InvoiceId,RepositoryId")]
            InvoiceItem invoiceItem)
        {

            if (ModelState.IsValid)
            {
                db.Entry(invoiceItem).State = EntityState.Modified;
                int temp = int.Parse(Session["quantity"].ToString());
                var prd = db.InvoiceItems.Find(invoiceItem.Id);
                var stockitem = db.StockItems
                    .Where(x => x.ProductId == invoiceItem.ProductId).FirstOrDefault();

                int dif = invoiceItem.Quantity - temp;
                #region Product Different
                //get stock item to return the quantity that be lost befor
                //'1' and '-1' means the invoice`s item there is
                //'0'  there isnt such this invoice`s item
                int prdDif = db.InvoiceItems
                    .Where(i => i.Id == invoiceItem.Id)
                    .Select(i => i.ProductId)
                    .FirstOrDefault()
                    .CompareTo(invoiceItem.ProductId);


                if (prdDif != 0)
                {
                    //if ((stockitem.Quantity - invoiceItem.Quantity) > 0)
                    //{

                        RollbackStockItem(invoiceItem);
                        TempData["message"] = "ویرایش انجام شد";
                        TempData["success"] = "true";

                        return Json(new { success = true });
                    //}

                    //TempData["message"] = "امکان ویرایش وجود ندارد";
                    //TempData["success"] = "false";

                    //return Json(new { success = true });

                }
                #endregion

                return DoBaseOnDiffer(invoiceItem, stockitem, dif);

                //return RedirectToAction("Index");
            }
            ViewBag.RepositoryId = new SelectList(db.Repositories, "Id", "Name",invoiceItem.RepositoryId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", invoiceItem.ProductId);
            TempData["message"] = "درخواست شما لغو شد";
            TempData["success"] = "false";
            return PartialView(viewName: "Edit", model: invoiceItem);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Delete", model: invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            db.InvoiceItems.Remove(invoiceItem);
            db.SaveChanges();

            var stockitme = db.StockItems.Where(x => x.ProductId == invoiceItem.ProductId)
                .FirstOrDefault();
            stockitme.Quantity += invoiceItem.Quantity;
            db.Entry(stockitme).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }

        #region Methods
        private void DoSctockItem(InvoiceItem invoiceItem, DomainModel.Order.StockItem stockitem)
        {
            stockitem.Quantity -= invoiceItem.Quantity;
            db.Entry(stockitem).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void GetInvoiceId()
        {
            PersianCalendar pc = new PersianCalendar();

            if (Session["InvoiceId"] == null)
            {
                Invoice invoice = new Invoice();
                invoice.CreatedDate = string.Format("{0}/{1}/{2}",
                    pc.GetYear(DateTime.Now),
                    pc.GetMonth(DateTime.Now),
                    pc.GetDayOfMonth(DateTime.Now));
                invoice.AppUserId = User.Identity.GetUserId<int>();

                db.Invoices.Add(invoice);
                db.SaveChanges();
                Session["InvoiceId"] = invoice.Id;
            }
        }

        /// <summary>
        /// It is for such situation that deponds on whether or no stock item
        /// </summary>
        /// <param name="invoiceItem"></param>
        /// <param name="stockitem"></param>
        /// <param name="dif"></param>
        /// <returns></returns>
        private ActionResult DoBaseOnDiffer(InvoiceItem invoiceItem, DomainModel.Order.StockItem stockitem, int dif)
        {
            if (dif > 0)
            {
                //if ((stockitem.Quantity - dif) > 0)
                //{
                    db.SaveChanges();
                    stockitem.Quantity -= dif;
                    db.Entry(stockitem).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["message"] = "ویرایش انجام شد";
                    TempData["success"] = "true";

                    return Json(new { success = true });
                //}
                //else
                //{
                //    TempData["message"] = "موجودی منفی می شود.ویرایش لغو شد";
                //    TempData["success"] = "false";

                //    return Json(new { success = true });

                //}
            }
            else
            {
                InsertNewInvoiceItem(dif, stockitem);
                TempData["message"] = "ویرایش انجام شد";
                TempData["success"] = "true";

                return Json(new { success = true });
            }
        }

        /// <summary>
        /// pass the invoice item and stock item to update them
        /// </summary>
        /// <param name="invoiceItem"></param>
        /// <param name="stockitem"></param>
        /// <exception cref="adsfasd"></exception>
        private void InsertNewInvoiceItem(int dif, DomainModel.Order.StockItem stockitem)
        {
            TempData["message"] = dif.ToString();
            TempData["success"] = true;
            stockitem.Quantity -= dif;
            db.SaveChanges();
            db.Entry(stockitem).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void RollbackStockItem(InvoiceItem invoiceItem)
        {
            int i = int.Parse(Session["productId"].ToString());
            var st = db.StockItems
                .Where(s => s.ProductId == i)
                .FirstOrDefault();
            st.Quantity += int.Parse(Session["quantity"].ToString());

            //update the invoice item
            db.SaveChanges();

            //roll back the quantity of stock item
            db.Entry(st).State = EntityState.Modified;
            db.SaveChanges();

            //کسر از موجودی بر اساس آیتم فعلی
            var stockItem = db.StockItems
                .Where(x => x.ProductId == invoiceItem.ProductId).FirstOrDefault();
            if (stockItem != null)
            {
                stockItem.Quantity -= invoiceItem.Quantity;
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                DomainModel.Order.StockItem stItme = new DomainModel.Order.StockItem()
                {
                    Price = invoiceItem.Price,
                    ProductId = invoiceItem.ProductId,
                    Quantity = invoiceItem.Quantity
                };
                db.StockItems.Add(stItme);
                db.SaveChanges();
            }

        }

        public ActionResult SelectPrice(int? id)
        {
            Session["product"] = id;
            var p = (from x in db.BuySlipItems
                     where (x.ProductId == id)
                     select x).OrderByDescending(x => x.Price);

            //var fee = db.ProductCategories.Find(id).Fee;
            var fee = db.Products.Include(x => x.ProductCategory)
                .Where(x => x.Id == id)
                .Select(x => x.ProductCategory.Fee).FirstOrDefault();
                
            var pv = (p.First().Price*(fee/100))+p.FirstOrDefault().Price;
           
            return Json(pv, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectQuantity(int? id)
        {
            int ids = int.Parse(Session["product"].ToString());
            var p = (from x in db.BuySlipItems
                     where (x.ProductId == ids)
                     select x).OrderByDescending(x => x.Price);

            //var fee = db.ProductCategories.Find(id).Fee;
            var package = db.Products.Include(x => x.ProductCategory)
                .Where(x => x.Id == ids)
                .Select(x => x.ProductCategory.PackageCount).FirstOrDefault();

            var pv = id/package;

            return Json(pv, JsonRequestBehavior.AllowGet);
        }
        #endregion

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
