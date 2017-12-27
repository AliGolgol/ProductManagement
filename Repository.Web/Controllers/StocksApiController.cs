using Repository.DataLayer.Context;
using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Repository.Web.Controllers
{
    [Authorize]
    public class StocksApiController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //public IEnumerable<pro> Get()
        //{
        //    var products = new List<pro>();
        //    for (var i = 1; i <= 100; i++)
        //    {
        //        products.Add(new pro { Id = i, Name = "Product " + i });
        //    }
        //    return products;
        //    //return db.Stocks.ToList();
        //}

        public IEnumerable<Stock> GetStock()
        {
            return db.Stocks.ToList();
        }
    }

}
