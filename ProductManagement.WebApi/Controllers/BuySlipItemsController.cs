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
using Repository.DomainModel.Entry;
using System.Globalization;
using Repository.DomainModel.Order;
using System.Net.Http.Headers;

namespace ProductManagement.WebApi.Controllers
{
    public class BuySlipItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public BuySlipItemsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/BuySlipItems
        public IQueryable<BuySlipItem> GetBuySlipItems()
        {

            return db.BuySlipItems;
        }

        // GET: api/BuySlipItems/5
        [ResponseType(typeof(BuySlipItem))]
        public async Task<IHttpActionResult> GetBuySlipItem(int id)
        {
            BuySlipItem buySlipItem = await db.BuySlipItems.FindAsync(id);
            if (buySlipItem == null)
            {
                return NotFound();
            }

            return Ok(buySlipItem);
        }

        // PUT: api/BuySlipItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBuySlipItem(int id, BuySlipItem buySlipItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buySlipItem.Id)
            {
                return BadRequest();
            }

            db.Entry(buySlipItem).State = EntityState.Modified;

            int dif = buySlipItem.Quantity - db.BuySlipItems.
                Where(x => x.Id == buySlipItem.Id).
                Select(x => x.Quantity).FirstOrDefault();
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!BuySlipItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var stockitem = db.StockItems.
                FirstOrDefault(x => x.ProductId == buySlipItem.ProductId);
            stockitem.Quantity += dif;
            db.Entry(stockitem).State = EntityState.Modified;
            await db.SaveChangesAsync();
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BuySlipItems
        [ResponseType(typeof(BuySlipItem))]
        public async Task<IHttpActionResult> PostBuySlipItem(BuySlipItem buySlipItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.BuySlipItems.Add(buySlipItem);
            //await db.SaveChangesAsync();
            #region Stock
            await DoStockAsync(buySlipItem);
            #endregion

            return CreatedAtRoute("DefaultApi", new { id = buySlipItem.Id }, buySlipItem);
        }       

        // DELETE: api/BuySlipItems/5
        [ResponseType(typeof(BuySlipItem))]
        public async Task<IHttpActionResult> DeleteBuySlipItem(int id)
        {
            BuySlipItem buySlipItem = await db.BuySlipItems.FindAsync(id);
            if (buySlipItem == null)
            {
                return NotFound();
            }

            db.BuySlipItems.Remove(buySlipItem);

            var stockitem = db.StockItems.
                Where(x => x.ProductId == buySlipItem.ProductId).
                FirstOrDefault();
            stockitem.Quantity -= buySlipItem.Quantity;

            //BuySlipItme
            await db.SaveChangesAsync();

            db.Entry(stockitem).State = EntityState.Modified;
            //SockItme
            await db.SaveChangesAsync();

            return Ok(buySlipItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuySlipItemExists(int id)
        {
            return db.BuySlipItems.Count(e => e.Id == id) > 0;
        }

        private async Task DoStockAsync(BuySlipItem buySlipItem)
        {
            db.BuySlipItems.Add(buySlipItem);
            await db.SaveChangesAsync();

            var stockItem = db.StockItems.Where(x => x.ProductId == buySlipItem.ProductId).
                FirstOrDefault();
            if (stockItem != null)
            {
                stockItem.Quantity += buySlipItem.Quantity;
                db.Entry(stockItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                StockItem st = new StockItem()
                {
                    ProductId = buySlipItem.ProductId,
                    Quantity = buySlipItem.Quantity
                };
                db.StockItems.Add(st);
                await db.SaveChangesAsync();
            }
        }

        #region method 

        private void GetBuySlipId()
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            cookie["ses"].Value = "1";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });


            PersianCalendar pc = new PersianCalendar();
            //if (Session["BuySlipId"] == null)
            //{
            //    BuySlip buyslip = new BuySlip();
            //    buyslip.DateCreation = string.Format("{0}/{1}/{2}",
            //        pc.GetYear(DateTime.Now),
            //        pc.GetMonth(DateTime.Now),
            //        pc.GetDayOfMonth(DateTime.Now));
            //    buyslip.AppUserId = User.Identity.GetUserId<int>();
            //    db.BuySlips.Add(buyslip);
            //    db.SaveChanges();
            //    Session["BuySlipId"] = buyslip.Id;
            //}
        }
        #endregion
    }
}