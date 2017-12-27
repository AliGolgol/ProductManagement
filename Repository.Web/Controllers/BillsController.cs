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
using System.Threading;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bills
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.PaymentType);
            return View(bills.ToList());
        }

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        public ActionResult Detail1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return PartialView(viewName:"_Detail", model:bill);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name");
            ViewBag.BiilTypeId = new SelectList(db.BillTypes, "Id", "Name");
            Bill bill = new Bill();
            return PartialView(viewName:"Create",model:bill);
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,CreatedDate,Description,Reciver,PeriedId,AppUserId,PaymentTypeId,BiilTypeId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                Session["billId"] = bill.Id;
                //return RedirectToAction("Create","BillItems", new { Id = bill.Id });
                return Json(new {success = true});
            }

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", bill.PaymentTypeId);
            //return View(bill);
            return Json(bill, JsonRequestBehavior.AllowGet);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                TempData["message"] = "درخواست نامعتبر";
                TempData["success"] = true;
                return RedirectToAction("Index", "Bills");

            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                TempData["message"] = "مورد یافت نشد";
                TempData["success"] = "false";
                return RedirectToAction("Index", "Bills");
            }
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", bill.PaymentTypeId);
            ViewBag.BiilTypeId = new SelectList(db.BillTypes, "Id", "Name", bill.BiilTypeId);
            return PartialView(viewName:"Edit",model: bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,Description,Reciver,PeriedId,AppUserId,PaymentTypeId,BiilTypeId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();

                if (bill.BillType.IsRemoval)
                {
                    
                }
                return RedirectToAction("Index");
            }
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", bill.PaymentTypeId);
            ViewBag.BiilTypeId = new SelectList(db.BillTypes, "Id", "Name", bill.BiilTypeId);
            return PartialView(viewName:"Edit", model:bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost] //برای این حالت امن‌تر است
        //[AjaxOnly]
        public ActionResult RenderModalPartialView()
        {
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name");
            ViewBag.BiilTypeId = new SelectList(db.BillTypes, "Id", "Name");
            //رندر پارشال ویوو صفحه مودال به همراه اطلاعات مورد نیاز آن
            return PartialView(viewName: "_RenderModalPartialView", model: new Bill
            {
                PaymentTypeId=ViewBag.PaymentTypeId,
                BiilTypeId=ViewBag.BillTypeId,
                CreatedDate="",
                Description=""
                
            });
        }

       
        public ActionResult BillItemInfo(int? id)
        {
            //Thread.Sleep(1000);
            var item = db.BillItems.Where(x => x.BillId == id).ToList();
            return PartialView(viewName: "_BillItemInfo", model: item);
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
