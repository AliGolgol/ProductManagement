using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using Repository.ViewModel.InvoiceItem;

namespace ProductManagement.WebApi.Controllers
{
    public class InvoiceItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public InvoiceItemsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/InvoiceItems
        public IQueryable<InvoiceItem> GetInvoiceItems()
        {
            return db.InvoiceItems;
        }

        // GET: api/InvoiceItems/5
        [ResponseType(typeof(InvoiceItem))]
        public async Task<IHttpActionResult> GetInvoiceItem(int id)
        {
            InvoiceItem invoiceItem = await db.InvoiceItems.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return Ok(invoiceItem);
        }

        [Route("~/api/InvoiceItems/{id}/Invoice")]
        [ResponseType(typeof(InvoiceItem))]
        public IQueryable<InvoiceItem> GetBuySlipItem(int id)
        {
            return db.InvoiceItems.Where(a => a.InvoiceId == id);
        }

        // PUT: api/InvoiceItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoiceItem(int id, InvoiceItem invoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceItem.Id)
            {
                return BadRequest();
            }

            db.Entry(invoiceItem).State = EntityState.Modified;


            var quantity = db.InvoiceItems.Where(s => s.Id == invoiceItem.Id)
                .Select(s => s.Quantity).FirstOrDefault();

            int dif = invoiceItem.Quantity - quantity;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var stock=db.StockItems.Where(s => s.ProductId == invoiceItem.ProductId).FirstOrDefault();
            db.Entry(stock).State = EntityState.Modified;
            stock.Quantity -= dif;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/InvoiceItems
        [ResponseType(typeof(InvoiceItem))]
        public async Task<IHttpActionResult> PostInvoiceItem(InvoiceItem invoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockitem = db.StockItems.
                          Where(x => x.ProductId == invoiceItem.ProductId)
                          .FirstOrDefault();

            var invoiceItemExist = db.InvoiceItems
                        .Where(x => x.InvoiceId == invoiceItem.InvoiceId
                        && x.ProductId == invoiceItem.ProductId).FirstOrDefault();

            if (invoiceItemExist != null)
            {
                //update invoice item
                invoiceItemExist.Quantity += invoiceItem.Quantity;

                //update stock item
                await DoStockItem(invoiceItem, stockitem);
                
            }
            else
            {
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();

                await DoStockItem(invoiceItem, stockitem);
               

            }

            //db.InvoiceItems.Add(invoiceItem);
            //await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = invoiceItem.Id }, invoiceItem);
        }

        // DELETE: api/InvoiceItems/5
        [ResponseType(typeof(InvoiceItem))]
        public async Task<IHttpActionResult> DeleteInvoiceItem(int id)
        {
            InvoiceItem invoiceItem = await db.InvoiceItems.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            db.InvoiceItems.Remove(invoiceItem);
            await db.SaveChangesAsync();

            var stockItem = db.StockItems.Where(s => s.ProductId == invoiceItem.ProductId).FirstOrDefault();
            db.Entry(stockItem).State = EntityState.Modified;
            stockItem.Quantity += invoiceItem.Quantity;
            await db.SaveChangesAsync();

            return Ok(invoiceItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceItemExists(int id)
        {
            return db.InvoiceItems.Count(e => e.Id == id) > 0;
        }

        private async Task DoStockItem(InvoiceItem invoiceItem, Repository.DomainModel.Order.StockItem stockitem)
        {
            stockitem.Quantity -= invoiceItem.Quantity;
            db.Entry(stockitem).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        [AcceptVerbs("GET")]
        [Route("~/api/InvoiceItems/{id}/SelectPrice")]
        public async Task<IHttpActionResult> SelectPrice(int? id)
        {
            //Session["product"] = id;
            var p = (from x in db.BuySlipItems
                     where (x.ProductId == id)
                     select x).OrderByDescending(x => x.Price);

            
            //var fee = db.ProductCategories.Find(id).Fee;
            var fee =await db.Products.Include(x => x.ProductCategory)
                .Where(x => x.Id == id)
                .Select(x => x.ProductCategory.Fee).FirstOrDefaultAsync();

            var pv = (p.FirstOrDefault().Price * (fee / 100)) + p.FirstOrDefault().Price;

            // return Json(pv, JsonRequestBehavior.AllowGet);
            return Ok(pv);
        }

        [AcceptVerbs("POST")]
        [Route("~/api/InvoiceItems/SelectQuantity")]
        public async Task<IHttpActionResult> SelectQuantity(SelectQuantity sq)
        {
            //int ids = int.Parse(Session["product"].ToString());
            var p = (from x in db.BuySlipItems
                     where (x.ProductId == sq.prdId)
                     select x).OrderByDescending(x => x.Price);

            //var fee = db.ProductCategories.Find(id).Fee;
            //var package = db.Products.Include(x => x.ProductCategory)
            //    .Where(x => x.Id == sq.prdId)
            //    .Select(x => x.ProductCategory.PackageCount).FirstOrDefault();

            var package = await db.Products.Include(x => x.ProductCategory)
                .Where(x => x.Id == sq.prdId)
                .Select(x => x.ProductCategory.PackageCount).FirstOrDefaultAsync();

            var pv = sq.id / package;

            //return Json(pv, JsonRequestBehavior.AllowGet);
            return Ok(pv);
        }
    }
}