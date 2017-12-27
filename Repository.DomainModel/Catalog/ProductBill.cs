using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Catalog
{
    public class ProductBill
    {
        public int Id { get; set; }
        public int ProrductId { get; set; }
        public int BillId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Bill Bill { get; set; }

    }
}
