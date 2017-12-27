using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Repository;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class RepositoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Repositories
        public ActionResult Index()
        {
            var repositories = db.Repositories.Include(r => r.RepositoryType);
            return View(repositories.ToList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Repository.Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Details",model:repository);
        }

        // GET: Repositories/Create
        public ActionResult Create()
        {
            ViewBag.RepositoryTypeId = new SelectList(db.RepositoryTypes, "Id", "Name");
            //ViewBag.PriceEstimate = new SelectList(db.Repositories.Include(x=>x.PriceEstimateId));
            DomainModel.Repository.Repository repository = new DomainModel.Repository.Repository();
            return PartialView(viewName:"Create", model:repository);
        }

        // POST: Repositories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,RepositoryTypeId,PriceEstimateId")] DomainModel.Repository.Repository repository)
        {
            if (ModelState.IsValid)
            {
                db.Repositories.Add(repository);
                db.SaveChanges();
                //return RedirectToAction("Index");
             return Json(new {success = true});
         }

            ViewBag.RepositoryTypeId = new SelectList(db.RepositoryTypes, "Id", "Name", repository.RepositoryTypeId);
            return PartialView(viewName:"Create",model:repository);
        }

        // GET: Repositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Repository.Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            ViewBag.RepositoryTypeId = new SelectList(db.RepositoryTypes, "Id", "Name", repository.RepositoryTypeId);
            return PartialView(viewName:"Edit",model:repository);
        }

        // POST: Repositories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,RepositoryTypeId,PriceEstimateId")] DomainModel.Repository.Repository repository)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repository).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new {success=true});
            }
            ViewBag.RepositoryTypeId = new SelectList(db.RepositoryTypes, "Id", "Name", repository.RepositoryTypeId);
            return PartialView(viewName:"Edit",model:repository);
        }

        // GET: Repositories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainModel.Repository.Repository repository = db.Repositories.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Delete",model:repository);
        }

        // POST: Repositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DomainModel.Repository.Repository repository = db.Repositories.Find(id);
            db.Repositories.Remove(repository);
            db.SaveChanges();
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
