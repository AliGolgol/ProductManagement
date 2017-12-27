using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Catalog
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="نام را وارد کنید")]
        [DisplayName("نام سازنده")]
        public string Name { get; set; }

        [DisplayName("آدرس")]
        public string Address { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("تلفن")]
        public string Tel { get; set; }

        public ICollection<Product> Produtcs { get; set; }
    }
}
