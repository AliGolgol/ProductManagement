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
    public class EntrySlipsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EntrySlips
        public IQueryable<EntrySlip> GetEntrySlips()
        {
            return db.EntrySlips;
        }

        // GET: api/EntrySlips/5
        [ResponseType(typeof(EntrySlip))]
        public async Task<IHttpActionResult> GetEntrySlip(int id)
        {
            EntrySlip entrySlip = await db.EntrySlips.FindAsync(id);
            if (entrySlip == null)
            {
                return NotFound();
            }

            return Ok(entrySlip);
        }

        // PUT: api/EntrySlips/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntrySlip(int id, EntrySlip entrySlip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entrySlip.Id)
            {
                return BadRequest();
            }

            db.Entry(entrySlip).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrySlipExists(id))
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

        // POST: api/EntrySlips
        [ResponseType(typeof(EntrySlip))]
        public async Task<IHttpActionResult> PostEntrySlip(EntrySlip entrySlip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EntrySlips.Add(entrySlip);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entrySlip.Id }, entrySlip);
        }

        // DELETE: api/EntrySlips/5
        [ResponseType(typeof(EntrySlip))]
        public async Task<IHttpActionResult> DeleteEntrySlip(int id)
        {
            EntrySlip entrySlip = await db.EntrySlips.FindAsync(id);
            if (entrySlip == null)
            {
                return NotFound();
            }

            db.EntrySlips.Remove(entrySlip);
            await db.SaveChangesAsync();

            return Ok(entrySlip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntrySlipExists(int id)
        {
            return db.EntrySlips.Count(e => e.Id == id) > 0;
        }
    }
}