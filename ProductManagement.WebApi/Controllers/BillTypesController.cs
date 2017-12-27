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
using Repository.DomainModel.Catalog;

namespace ProductManagement.WebApi.Controllers
{
    public class BillTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BillTypes
        public IQueryable<BillType> GetBillTypes()
        {
            return db.BillTypes;
        }

        // GET: api/BillTypes/5
        [ResponseType(typeof(BillType))]
        public async Task<IHttpActionResult> GetBillType(int id)
        {
            BillType billType = await db.BillTypes.FindAsync(id);
            if (billType == null)
            {
                return NotFound();
            }

            return Ok(billType);
        }

        // PUT: api/BillTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBillType(int id, BillType billType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billType.Id)
            {
                return BadRequest();
            }

            db.Entry(billType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillTypeExists(id))
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

        // POST: api/BillTypes
        [ResponseType(typeof(BillType))]
        public async Task<IHttpActionResult> PostBillType(BillType billType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BillTypes.Add(billType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = billType.Id }, billType);
        }

        // DELETE: api/BillTypes/5
        [ResponseType(typeof(BillType))]
        public async Task<IHttpActionResult> DeleteBillType(int id)
        {
            BillType billType = await db.BillTypes.FindAsync(id);
            if (billType == null)
            {
                return NotFound();
            }

            db.BillTypes.Remove(billType);
            await db.SaveChangesAsync();

            return Ok(billType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BillTypeExists(int id)
        {
            return db.BillTypes.Count(e => e.Id == id) > 0;
        }
    }
}