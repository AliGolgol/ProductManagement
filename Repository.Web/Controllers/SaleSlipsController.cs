using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Repository.DataLayer.Context;
using Repository.ViewModel.Order;
using Repository.DomainModel.Order;
using Repository.DomainModel.Catalog;
using PagedList;
using System.Collections.Generic;
using EntityFramework.Extensions;
using System.Net;
using Repository.ServiceLayer.Contracts;
using Repository.ServiceLayer;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class SaleSlipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IApplicationUserManager _userMageer;
        private readonly IApplicationRoleManager _roleManager;
        public SaleSlipsController(IApplicationUserManager userManager,IApplicationRoleManager roleManager)
        {
            _userMageer = userManager;
            _roleManager = roleManager;
        }
        // GET: SaleSlips
        public ActionResult Index(int? page)
        {

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var stocks = db.Stocks.Include(x => x.StockItems).OrderBy(x=>x.Id);

            return View(stocks.ToPagedList(pageNumber,pageSize));
            //return Json(stocks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get()
        {
            var stocks = db.Stocks.ToList();
            return Json(stocks, JsonRequestBehavior.AllowGet);
        }

        // GET: SaleSlips/Details/5
        public ActionResult Details(int id)
        {
            var st = new StockItemViewModel()
            {
                Stocks=db.Stocks.Find(id),
                //StockItems=db.StockItems.Where(s=>s.StockId== id).ToList()
            };

            return View(st);
        }

        // GET: SaleSlips/Create
        public  ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name");

            //to do that use asyn Task<ActionResult> methodName()
            //var x = await _userMageer.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
            var xx = User.Identity.GetUserId();
            return View();
        }

        // POST: SaleSlips/Create
        [HttpPost]
        public ActionResult Create(DomainModel.Order.StockItem stockItem, FormCollection collection)
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name");

            GetPeriod();
            GetStock();

            #region Save and contunue
            if (Request.Form["btnContinue"] != null && ModelState.IsValid)
            {
                
                //stockItem.StockId = int.Parse(Session["StockId"].ToString());
                db.StockItems.Add(stockItem);
                db.SaveChanges();

                ViewBag.Info = stockItem.Quantity + "-" + stockItem.Id;

                //update the product to increase quantity,if there is
                Product prd = db.Products.Where(p => p.Id == stockItem.ProductId).First();
                if (prd != null)
                {
                    prd.QuantityPerUnit += stockItem.Quantity;
                    db.Entry(prd).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //create a new product
                    Product prdNew = new Product()
                    {
                        //Price = stockItem.Price.ToString(),
                        //QuantityPerUnit = stockItem.Quantity,
                        //ProductCategoryId = stockItem.ProductCategoryId,
                        ManufacturerId = db.Manufacturers.Select(x=>x.Id).First()
                    };
                    db.Products.Add(prdNew);
                    db.SaveChanges();
                    
                }
                return View();
            }
            #endregion

            #region Save
            if (Request.Form["btnSave"] != null)
            {
                
                //stockItem.StockId = int.Parse(Session["StockId"].ToString());
                db.StockItems.Add(stockItem);
                db.SaveChanges();
                
                Product prd = db.Products.Find(stockItem.ProductId);
                prd.QuantityPerUnit += stockItem.Quantity;
                db.Entry(prd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "SaleSlips", new { Id = int.Parse(Session["StockId"].ToString()) });
            }
            #endregion

            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        private void GetStock()
        {
            if (Session["StockId"] == null)
            {
                Stock st = new Stock()
                {
                    Description = "test",
                    CreatedDate = "1395/03/09",
                    PeriodId = int.Parse(Session["PeriodId"].ToString())
                };
                db.Stocks.Add(st);
                db.SaveChanges();
                int stockId = st.Id;
                Session["StockId"] = stockId;
            }
        }

        private void GetPeriod()
        {
            if (Session["PeriodId"] == null)
            {
                //Period pe = new Period()
                //{
                //    Name = "test",
                //    EndDate = "1395/12/29",
                //    StartDate = "1305/01/01"
                //};
                //db.Periods.Add(pe);
                //db.SaveChanges();
                //int perId = pe.Id;
                var pe = db.Periods.First();
                Session["PeriodId"] = pe.Id;
            }


        }

        // GET: SaleSlips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock==null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: SaleSlips/Edit/5
        [HttpPost]
        public ActionResult Edit(Stock stock)
        {
            stock.PeriodId = db.Periods.Select(p => p.Id).First();

            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // GET: SaleSlips/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SaleSlips/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                //db.StockItems.Where(x => x.StockId == id).Delete();
                db.SaveChanges();

                db.Stocks.Where(x => x.Id == id).Delete();
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
