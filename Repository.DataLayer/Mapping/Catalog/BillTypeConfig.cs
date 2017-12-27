using Repository.DomainModel.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class BillTypeConfig:EntityTypeConfiguration<BillType>
    {
        public BillTypeConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Description)
                .HasMaxLength(500);
        }
    }
}
