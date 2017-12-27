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
using Repository.DomainModel.Common;

namespace ProductManagement.WebApi.Controllers
{
    public class PaymentTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PaymentTypes
        public IQueryable<PaymentType> GetPaymentTypes()
        {
            return db.PaymentTypes;
        }

        // GET: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public async Task<IHttpActionResult> GetPaymentType(int id)
        {
            PaymentType paymentType = await db.PaymentTypes.FindAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaymentType(int id, PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            db.Entry(paymentType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypes
        [ResponseType(typeof(PaymentType))]
        public async Task<IHttpActionResult> PostPaymentType(PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentTypes.Add(paymentType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public async Task<IHttpActionResult> DeletePaymentType(int id)
        {
            PaymentType paymentType = await db.PaymentTypes.FindAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            db.PaymentTypes.Remove(paymentType);
            await db.SaveChangesAsync();

            return Ok(paymentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentTypeExists(int id)
        {
            return db.PaymentTypes.Count(e => e.Id == id) > 0;
        }
    }
}