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
using Repository.DomainModel.Repository;
using Repository.ServiceLayer.Contracts;

namespace ProductManagement.WebApi.Controllers
{
    [RoutePrefix("repositoryTypes")]
    public class RepositoryTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        IRepositoryTypeService _repositoryTypeService;
        IUnitOfWork _uow;
        public RepositoryTypesController(IUnitOfWork uow, IRepositoryTypeService repositoryType)
        {
            _uow = uow;
            _repositoryTypeService = repositoryType;
        }

        public RepositoryTypesController()
        {

        }
        // GET: api/RepositoryTypes
        public IEnumerable<RepositoryType> GetRepositoryTypes()
        {
            return _repositoryTypeService.GetAll();
            //return db.RepositoryTypes;
        }

        // GET: api/RepositoryTypes/5
        [ResponseType(typeof(RepositoryType))]
        public async Task<IHttpActionResult> GetRepositoryType(int id)
        {
            RepositoryType repositoryType = await db.RepositoryTypes.FindAsync(id);
            //RepositoryType repositoryType=await _repositoryTypeService.
            if (repositoryType == null)
            {
                return NotFound();
            }

            return Ok(repositoryType);
        }

        // PUT: api/RepositoryTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRepositoryType(int id, RepositoryType repositoryType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != repositoryType.Id)
            {
                return BadRequest();
            }

            db.Entry(repositoryType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryTypeExists(id))
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

        // POST: api/RepositoryTypes
        [ResponseType(typeof(RepositoryType))]
        public async Task<IHttpActionResult> PostRepositoryType(RepositoryType repositoryType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RepositoryTypes.Add(repositoryType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = repositoryType.Id }, repositoryType);
        }

        // DELETE: api/RepositoryTypes/5
        [ResponseType(typeof(RepositoryType))]
        public async Task<IHttpActionResult> DeleteRepositoryType(int id)
        {
            RepositoryType repositoryType = await db.RepositoryTypes.FindAsync(id);
            if (repositoryType == null)
            {
                return NotFound();
            }

            db.RepositoryTypes.Remove(repositoryType);
            await db.SaveChangesAsync();

            return Ok(repositoryType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RepositoryTypeExists(int id)
        {
            return db.RepositoryTypes.Count(e => e.Id == id) > 0;
        }
    }
}