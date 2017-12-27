using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using Repository.ViewModel.Order;
using System.Linq.Dynamic;
using Repository.ViewModel.Common;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class StockItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockItems
        public ActionResult Index(string filter = null, int page = 1,
            int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<ViewModel.Order.StockItemListViewModel>();
            ViewBag.filter = filter;
            records.Content = db.StockItems
                            .Where(x => filter == null ||
                            (x.Products.Name.Contains(filter)))
                            .OrderBy(sort+" "+sortdir)
                            .Skip((page-1)*pageSize)
                            .Take(pageSize)
                            .Select(x=> new ViewModel.Order.StockItemListViewModel
                            {
                                Id=x.Id,
                                Name=x.Products.Name,
                                RepName=x.Quantity.ToString(),
                                Quantity=x.Quantity.ToString()
                            }
                            )
                            .ToList();
            //count
            records.TotalRecords = db.StockItems
                                 .Where(x => filter == null ||
                            (x.Products.Name.Contains(filter))).Count();
            records.PageSize = pageSize;
            records.HeaderType = db.StockItems.Select(x => x.Products.Name).ToList();
            var stockItems = db.StockItems.Include(s => s.Products);
            return View(records);
        }

        // GET: StockItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Order.StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // GET: StockItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,Price,ProductId")] DomainModel.Order.StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.StockItems.Add(stockItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", stockItem.ProductId);
            return View(stockItem);
        }

        // GET: StockItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Order.StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", stockItem.ProductId);
            return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,Price,ProductId")] DomainModel.Order.StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", stockItem.ProductId);
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Order.StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DomainModel.Order.StockItem stockItem = db.StockItems.Find(id);
            db.StockItems.Remove(stockItem);
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
