using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Repository.DataLayer.Context;
using Repository.DomainModel.Entry;
using System.Linq.Dynamic;
using Repository.ViewModel.Common;

namespace ProductManagement.WebApi.Controllers
{
    public class BuySlipsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public BuySlipsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/BuySlips
        [Route("~/api/BuySlips/GetBuySlips")]
        [AcceptVerbs("GET","POST")]
        public async Task<IHttpActionResult> GetBuySlips(PageList p)
        {
            var slip = await db.BuySlips.OrderBy(x => x.Id)
                .Skip((p.CurrentPage - 1) * p.ItemsPerPage)
                .Take(p.ItemsPerPage).ToListAsync();

            //return db.BuySlips.Include(b => b.Period).Include(b => b.Supplier)
            //    .Include(b => b.EntrySlipType); 

            return Ok(slip);
        }

        // GET: api/BuySlips/5
        [ResponseType(typeof(BuySlip))]
        public async Task<IHttpActionResult> GetBuySlip(int id)
        {
            BuySlip buySlip = await db.BuySlips.Include(s => s.BuySlipItems)
                .Where(xs => xs.Id == id).SingleAsync();
            
            if (buySlip == null)
            {
                return NotFound();
            }

            return Ok(buySlip);
        }
       
        [Route("~/api/LastBuySLip")]
        [ResponseType(typeof(BuySlip))]
        public  async Task<IHttpActionResult> GetLast() {
            var buy = await db.BuySlips.OrderByDescending(x=>x.Id).FirstAsync();
            return  Ok(buy);
        }


        [Route("~/api/BuySlipItems/{id}/BuySlip")]
        [ResponseType(typeof(BuySlipItem))]
        public IQueryable<BuySlipItem> GetBuySlipItem(int id)
        {
            IQueryable<BuySlipItem> buyItem;
            if (id != 0)
            {
                buyItem = db.BuySlipItems.Where(a => a.BuySlipId == id);
            }
            else
            {
                int slipId = db.BuySlips.LastOrDefault().Id;
                buyItem = db.BuySlipItems.Where(a => a.BuySlipId == slipId);


            }

            //if (buySlitItems == null)
            //{
            //    return NotFound();
            //}

            return buyItem;
        }

        // PUT: api/BuySlips/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBuySlip(int id, BuySlip buySlip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buySlip.Id)
            {
                return BadRequest();
            }

            db.Entry(buySlip).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuySlipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BuySlips
        [ResponseType(typeof(BuySlip))]
        public async Task<IHttpActionResult> PostBuySlip(BuySlip buySlip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BuySlips.Add(buySlip);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = buySlip.Id }, buySlip);
        }

        // DELETE: api/BuySlips/5
        [ResponseType(typeof(BuySlip))]
        public async Task<IHttpActionResult> DeleteBuySlip(int id)
        {
            BuySlip buySlip = await db.BuySlips.FindAsync(id);
            if (buySlip == null)
            {
                return NotFound();
            }

            db.BuySlips.Remove(buySlip);
            await db.SaveChangesAsync();

            return Ok(buySlip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuySlipExists(int id)
        {
            return db.BuySlips.Count(e => e.Id == id) > 0;
        }
    }
}