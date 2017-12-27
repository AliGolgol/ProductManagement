using Repository.DataLayer.Context;
using Repository.DomainModel.Common;
using Repository.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        IAboutService _aboutSrvice;
        IUnitOfWork _uow;

        public AboutController()
        {}

        public AboutController(IUnitOfWork uow, IAboutService aboutService)
        {
            _uow = uow;
            _aboutSrvice = aboutService;
        }

        // GET: About
        public ActionResult Index()
        {
            var about = _aboutSrvice.GetAll();
            return View(about);
        }

        // GET: About/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: About/Create
        public ActionResult Create()
        {
            About about = new About();
            return PartialView(about);
        }

        // POST: About/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Tel")]About about, FormCollection collection)
        {
            if (db.Abouts.Any())
            {
                TempData["message"] = "شما می توانید یک فروشگاه ایجاد کنید";
                TempData["success"] = "false";
                return Json(new { success = true });
            }
            if (ModelState.IsValid)
            {
                db.Abouts.Add(about);
                _aboutSrvice.Add(about);
                //db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView(viewName: "Create", model: about);
        }

        // GET: About/Edit/5
        public ActionResult Edit(int id)
        {
            //var about = db.Abouts.Find(id);
            var about = _aboutSrvice.Get(id);
            return PartialView(viewName:"Create",model:about);
        }

        // POST: About/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Tel")]About about)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(about).State = EntityState.Modified;
                _aboutSrvice.Update(about);
                //db.SaveChanges();
                return Json(new { success = true });
            }
            TempData["message"] = "شما می توانید یک فروشگاه ایجاد کنید";
            TempData["success"] = "false";
            return PartialView(viewName: "Edit", model: about);
        }       
    }
}
