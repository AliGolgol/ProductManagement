using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.Common
{
    public class PaginatedResult<T>
    {
        public List<T> result { get; set; }
        public PageList pagination { get; set; }
    }
}
