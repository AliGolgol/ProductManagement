using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Entry;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class EntrySlipTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntrySlipTypes
        public ActionResult Index()
        {
            return View(db.EntrySlipTypes.ToList());
        }

        // GET: EntrySlipTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipType entrySlipType = db.EntrySlipTypes.Find(id);
            if (entrySlipType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Details",model:entrySlipType);
        }

        // GET: EntrySlipTypes/Create
        public ActionResult Create()
        {
            EntrySlipType est = new EntrySlipType();
            return PartialView(viewName:"Create",model:est);
        }

        // POST: EntrySlipTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] EntrySlipType entrySlipType)
        {
            if (ModelState.IsValid)
            {
                db.EntrySlipTypes.Add(entrySlipType);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView(viewName: "Create", model: entrySlipType);
        }

        // GET: EntrySlipTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipType entrySlipType = db.EntrySlipTypes.Find(id);
            if (entrySlipType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Edit",model:entrySlipType);
        }

        // POST: EntrySlipTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EntrySlipType entrySlipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrySlipType).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView(viewName:"Edit",model:entrySlipType);
        }

        // GET: EntrySlipTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlipType entrySlipType = db.EntrySlipTypes.Find(id);
            if (entrySlipType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Delete",model:entrySlipType);
        }

        // POST: EntrySlipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntrySlipType entrySlipType = db.EntrySlipTypes.Find(id);
            db.EntrySlipTypes.Remove(entrySlipType);
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
