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
using Repository.ViewModel.Common;
using System.Linq.Dynamic;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class EntrySlipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntrySlips
        public ActionResult Index(string filter = null, int page = 1,
            int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<EntrySlip>();
            ViewBag.filter = filter;
            records.Content = db.EntrySlips
                            .Where(x => filter == null ||
                            (x.CreatedDate.Contains(filter))||
                            (x.Description.Contains(filter))||
                            (x.DeliveryMan.Contains(filter)))
                            .OrderBy(sort+" "+sortdir)
                            .Skip((page-1)*pageSize)
                            .Take(pageSize).ToList();

            var entrySlips = db.EntrySlips.Include(e => e.Period);
            return View(entrySlips.ToList());
        }

        // GET: EntrySlips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlip entrySlip = db.EntrySlips.Find(id);
            if (entrySlip == null)
            {
                return HttpNotFound();
            }
            return View(entrySlip);
        }

        // GET: EntrySlips/Create
        public ActionResult Create()
        {
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name");
            ViewBag.ESTypeId = new SelectList(db.EntrySlipTypes, "Id", "Name");
            return View();
        }

        // POST: EntrySlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedDate,Description,DeliveryMan,UserId,ESTypeId,PeriodId")] EntrySlip entrySlip)
        {
            if (ModelState.IsValid)
            {
                db.EntrySlips.Add(entrySlip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", entrySlip.PeriodId);
            return View(entrySlip);
        }

        // GET: EntrySlips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlip entrySlip = db.EntrySlips.Find(id);
            if (entrySlip == null)
            {
                return HttpNotFound();
            }
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", entrySlip.PeriodId);
            ViewBag.ESTypeId = new SelectList(db.EntrySlipTypes, "Id", "Name", entrySlip.ESTypeId);
            return View(entrySlip);
        }

        // POST: EntrySlips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,Description,DeliveryMan,UserId,ESTypeId,PeriodId")] EntrySlip entrySlip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrySlip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", entrySlip.PeriodId);
            return View(entrySlip);
        }

        // GET: EntrySlips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntrySlip entrySlip = db.EntrySlips.Find(id);
            if (entrySlip == null)
            {
                return HttpNotFound();
            }
            return View(entrySlip);
        }

        // POST: EntrySlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntrySlip entrySlip = db.EntrySlips.Find(id);
            db.EntrySlips.Remove(entrySlip);
            db.SaveChanges();
            return RedirectToAction("Index");
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
