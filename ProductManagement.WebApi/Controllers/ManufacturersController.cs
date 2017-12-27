using System;
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
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Repository.ViewModel.Common;
using System.Linq.Dynamic;

namespace ProductManagement.WebApi.Controllers
{

    //[Authorize]
    public class ManufacturersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        [Route("~/api/Manufacturers/GetManufacturers")]
        [AcceptVerbs("GET", "POST")]
        // GET: api/Manufacturers
        //[Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetManufacturers(PageList p)
        {
           
            var m = await db.Manufacturers.OrderBy(x => x.Id)
                .Skip((p.CurrentPage - 1) * p.ItemsPerPage)
                .Take(p.ItemsPerPage).ToListAsync();
            return Ok(m);
        }

        [Route("~/api/Manufacturers/GetMan")]
        [AcceptVerbs("GET", "POST")]
        public IQueryable<Manufacturer> GetMan()
        {
            return db.Manufacturers;
        }
        // GET: api/Manufacturers/5
        [ResponseType(typeof(Manufacturer))]
        public async Task<IHttpActionResult> GetManufacturer(int id)
        {
            //HttpClientHandler handler = new HttpClientHandler()
            //{
            //    UseCookies = true,
            //    UseDefaultCredentials = true,
            //    CookieContainer = new CookieContainer()
            //};

            //HttpClient client = new HttpClient(handler);
            //HttpResponseMessage response = await client.GetAsync("http://localhost:1912");
            //var coo = ActionContext.Request.Headers.GetCookies("cookieName");

            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return Ok(manufacturer);
        }

        // PUT: api/Manufacturers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturer.Id)
            {
                return BadRequest();
            }

            db.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        [ResponseType(typeof(Manufacturer))]
        public async Task<IHttpActionResult> PostManufacturer(Manufacturer manufacturer)
        {

            HttpResponseMessage respMessage = new HttpResponseMessage();
            respMessage.Content = new ObjectContent<string[]>(new string[] { "value1", "value2" },
                new JsonMediaTypeFormatter());
            CookieHeaderValue cookie = new CookieHeaderValue("session-id", "123");
            cookie.Expires = DateTimeOffset.Now.AddDays(2);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Manufacturers.Add(manufacturer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = manufacturer.Id }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [ResponseType(typeof(Manufacturer))]
        public async Task<IHttpActionResult> DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            db.Manufacturers.Remove(manufacturer);
            await db.SaveChangesAsync();

            return Ok(manufacturer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManufacturerExists(int id)
        {
            return db.Manufacturers.Count(e => e.Id == id) > 0;
        }
        //public HttpResponseMessage GetHtt()
        //{
        //    HttpResponseMessage respMessage = new HttpResponseMessage();
        //    respMessage.Content = new ObjectContent<string[]>(new string[] { "value1", "value2" },
        //        new JsonMediaTypeFormatter());
        //    CookieHeaderValue cookie = new CookieHeaderValue("session-id", "123");
        //    cookie.Expires = DateTimeOffset.Now.AddDays(2);
        //    cookie.Domain = Request.RequestUri.Host;
        //    cookie.Path = "/";
        //    respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        //    return respMessage;
        //}
    }
}