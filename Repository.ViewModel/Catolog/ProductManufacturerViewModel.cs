using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Catalog;

namespace Repository.ViewModel.Catolog
{
    public class ProductManufacturerViewModel
    {
        public Product Product { get; set; }
        public ICollection<Manufacturer> Manufacturers1 { get; set; }
        public ICollection<ProductCategory> ProductCategories1 { get; set; }
    }
}
