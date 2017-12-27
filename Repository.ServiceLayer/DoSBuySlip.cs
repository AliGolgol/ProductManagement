using Repository.DataLayer.Context;
using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServiceLayer
{
    public class DoSBuySlip
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void DoStock(BuySlipItem buySlipItem)
        {
            db.BuySlipItems.Add(buySlipItem);
            db.SaveChanges();

            var stockItem = db.StockItems.Where(x => x.ProductId == buySlipItem.ProductId).
                FirstOrDefault();
            if (stockItem != null)
            {
                stockItem.Quantity += buySlipItem.Quantity;
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                StockItem st = new StockItem()
                {
                    ProductId = buySlipItem.ProductId,
                    Quantity = buySlipItem.Quantity
                };
                db.StockItems.Add(st);
                db.SaveChanges();
            }
        }
    }
}
