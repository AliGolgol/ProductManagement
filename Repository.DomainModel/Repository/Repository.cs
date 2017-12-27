using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Repository
{
    public class Repository
    {
        public int Id { get; set; }
        [DisplayName("کد انبار")]
        public string Code { get; set; }
        [Required(ErrorMessage = "نام انبار را وارد کنید")]
        [DisplayName("نام انبار")]
        public string Name { get; set; }
        //public Repository Parent { get; set; }
        //public int ParentId { get; set; }

        public virtual RepositoryType RepositoryType { get; set; }
        public int? RepositoryTypeId { get; set; }

        public int PriceEstimateId { get; set; }
        public ICollection<EntrySlipItem> EntrySlipItems { get; set; }

        public ICollection<BuySlipItem> BuySlipItems { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        [NotMapped]
        public PriceEstimate PriceEstimate
        {
            get { return (PriceEstimate) PriceEstimateId; }
            set { PriceEstimateId=(int)value;
            }
            
        }
    }
}
