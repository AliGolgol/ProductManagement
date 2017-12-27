using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.DomainModel.Catalog;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class BillTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillTypes
        public ActionResult Index()
        {
            return View(db.BillTypes.ToList());
        }

        // GET: BillTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Details", model: billType);
        }

        // GET: BillTypes/Create
        public ActionResult Create()
        {
            BillType billType = new BillType();
            return PartialView(viewName:"Create",model:billType);
        }

        // POST: BillTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsRemoval")] BillType billType)
        {
            if (ModelState.IsValid)
            {
                db.BillTypes.Add(billType);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new {success = true});
            }
            if (billType.Name=="ali")
            {
                ModelState.AddModelError("", "لطفا فرم را تکمیل کنید");
            }
            return PartialView(viewName:"Create",model:billType);
        }

        // GET: BillTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"Edit", model: billType);
        }

        // POST: BillTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsRemoval")] BillType billType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billType).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new {success=true});
            }
            return PartialView(viewName:"Edit",model:billType);
        }

        // GET: BillTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName: "Delete", model: billType);
        }

        // POST: BillTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillType billType = db.BillTypes.Find(id);
            db.BillTypes.Remove(billType);
            db.SaveChanges();
            return Json(new {success=true});
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
