using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Repository;
using Repository.ServiceLayer.Contracts;
using Repository.ServiceLayer.EfServices;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class RepositoryTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        IRepositoryTypeService _repositoryTypeService;
        IUnitOfWork _uow;

        public RepositoryTypesController(IUnitOfWork uow, IRepositoryTypeService repositoryType)
        {
            _uow = uow;
            _repositoryTypeService = repositoryType;
        }

        // GET: RepositoryTypes
        public ActionResult Index()
        {
            var list = _repositoryTypeService.GetAll();
            return View(list);
        }

        // GET: RepositoryTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            RepositoryType repositoryType = _repositoryTypeService.Find(x => x.Id == id);
            if (repositoryType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Details",model:repositoryType);
        }

        // GET: RepositoryTypes/Create
        public ActionResult Create()
        {
            RepositoryType repositoryType = new RepositoryType();
            return PartialView(viewName: "Create", model: repositoryType);
        }

        // POST: RepositoryTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] RepositoryType repositoryType)
        {
            if (ModelState.IsValid)
            {
                _repositoryTypeService.Add(repositoryType);
                _uow.SaveAllChanges();
                return Json(new { success = true });
            }

            return PartialView(viewName:"Create",model:repositoryType);
        }

        // GET: RepositoryTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepositoryType repositoryType = _repositoryTypeService.Find(x => x.Id == id);
            if (repositoryType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Edit", model:repositoryType);
        }

        // POST: RepositoryTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] RepositoryType repositoryType)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(repositoryType).State = EntityState.Modified;
                //db.SaveChanges();
                _repositoryTypeService.Update(repositoryType);
                _uow.SaveAllChanges();
                return Json(new { success = true });
            }
            return PartialView(viewName:"Edit",model:repositoryType);
        }

        // GET: RepositoryTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepositoryType repositoryType = _repositoryTypeService.Find(x => x.Id == id);
            if (repositoryType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Delete",model:repositoryType);
        }

        // POST: RepositoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepositoryType repositoryType = _repositoryTypeService.Find(x => x.Id == id);
            _repositoryTypeService.Delete(repositoryType);
            _uow.SaveAllChanges();
            
            return Json(new {success=true});
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
