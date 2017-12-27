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

namespace ProductManagement.WebApi.Controllers
{
    public class EntrySlipTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EntrySlipTypes
        public IQueryable<EntrySlipType> GetEntrySlipTypes()
        {
            return db.EntrySlipTypes;
        }

        // GET: api/EntrySlipTypes/5
        [ResponseType(typeof(EntrySlipType))]
        public async Task<IHttpActionResult> GetEntrySlipType(int id)
        {
            EntrySlipType entrySlipType = await db.EntrySlipTypes.FindAsync(id);
            if (entrySlipType == null)
            {
                return NotFound();
            }

            return Ok(entrySlipType);
        }

        // PUT: api/EntrySlipTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntrySlipType(int id, EntrySlipType entrySlipType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entrySlipType.Id)
            {
                return BadRequest();
            }

            db.Entry(entrySlipType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrySlipTypeExists(id))
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

        // POST: api/EntrySlipTypes
        [ResponseType(typeof(EntrySlipType))]
        public async Task<IHttpActionResult> PostEntrySlipType(EntrySlipType entrySlipType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EntrySlipTypes.Add(entrySlipType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entrySlipType.Id }, entrySlipType);
        }

        // DELETE: api/EntrySlipTypes/5
        [ResponseType(typeof(EntrySlipType))]
        public async Task<IHttpActionResult> DeleteEntrySlipType(int id)
        {
            EntrySlipType entrySlipType = await db.EntrySlipTypes.FindAsync(id);
            if (entrySlipType == null)
            {
                return NotFound();
            }

            db.EntrySlipTypes.Remove(entrySlipType);
            await db.SaveChangesAsync();

            return Ok(entrySlipType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntrySlipTypeExists(int id)
        {
            return db.EntrySlipTypes.Count(e => e.Id == id) > 0;
        }
    }
}