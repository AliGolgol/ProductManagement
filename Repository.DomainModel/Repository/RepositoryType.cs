using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Repository
{
    public class RepositoryType
    {
        public int Id { get; set; }
        [DisplayName("نوع انبار")]
        [Required(ErrorMessage = "نوع انبار را وارد کنید")]
        public string Name { get; set; }
        public  ICollection<Repository> Repositories { get; set; }
    }
}
