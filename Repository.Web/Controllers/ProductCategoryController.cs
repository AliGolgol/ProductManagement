using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Catalog;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductCategory
        public ActionResult Index()
        {
            var productCategories = db.ProductCategories.Include(p => p.Parent);
            return View(productCategories.ToList());
        }

        // GET: ProductCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Details",model:productCategory);
        }

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.ProductCategories, "Id", "Name");
            
            return PartialView("Create");
        }

        // POST: ProductCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Description,ParentId,PackageCount,MinimumBalance,Fee,IsLastLevel")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                TempData["message"] = "درخواست شما انجام شد";
                TempData["success"] = "true";
                return Json(new { success = true });
            }

            ViewBag.ParentId = new SelectList(db.ProductCategories, "Id", "Name", productCategory.ParentId);

            return PartialView(viewName:"Create",model:productCategory);
        }

        // GET: ProductCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.ProductCategories, "Id", "Name", productCategory.ParentId);
            return PartialView(viewName:"Edit",model:productCategory);
        }

        // POST: ProductCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,Description,ParentId,PackageCount,MinimumBalance,Fee,IsLastLevel")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            ViewBag.ParentId = new SelectList(db.ProductCategories, "Id", "Name", productCategory.ParentId);
            return PartialView(viewName:"Edit",model:productCategory);
        }

        // GET: ProductCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Delete",model:productCategory);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productCategory);
            db.SaveChanges();
            TempData["message"] = "درخواست شما انجام شد";
            TempData["success"] = "true";
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
