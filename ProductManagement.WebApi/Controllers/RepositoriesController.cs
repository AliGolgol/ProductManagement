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

namespace ProductManagement.WebApi.Controllers
{
    public class RepositoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Repositories
        public IQueryable<Repository.DomainModel.Repository.Repository> GetRepositories()
        {
            return db.Repositories.Include(r => r.RepositoryType); ;
        }

        // GET: api/Repositories/5
        [ResponseType(typeof(Repository.DomainModel.Repository.Repository))]
        public async Task<IHttpActionResult> GetRepository(int id)
        {
            Repository.DomainModel.Repository.Repository repository = 
                await db.Repositories.FindAsync(id);
            if (repository == null)
            {
                return NotFound();
            }

            return Ok(repository);
        }

        // PUT: api/Repositories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRepository(int id, Repository.DomainModel.Repository.Repository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != repository.Id)
            {
                return BadRequest();
            }

            //repository.Code = "17";
            //repository.Name = "testCodeBehinde";
            //repository.RepositoryTypeId= 1;
            //repository.PriceEstimateId = 3;
            db.Entry(repository).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryExists(id))
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

        // POST: api/Repositories
        [ResponseType(typeof(Repository.DomainModel.Repository.Repository))]
        public async Task<IHttpActionResult> PostRepository(Repository.DomainModel.Repository.Repository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Repositories.Add(repository);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = repository.Id }, repository);
        }

        // DELETE: api/Repositories/5
        [ResponseType(typeof(Repository.DomainModel.Repository.Repository))]
        public async Task<IHttpActionResult> DeleteRepository(int id)
        {
            Repository.DomainModel.Repository.Repository repository = await db.Repositories.FindAsync(id);
            if (repository == null)
            {
                return NotFound();
            }

            db.Repositories.Remove(repository);
            await db.SaveChangesAsync();

            return Ok(repository);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RepositoryExists(int id)
        {
            return db.Repositories.Count(e => e.Id == id) > 0;
        }
    }
}