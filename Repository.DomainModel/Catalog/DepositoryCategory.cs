using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Catalog
{
    public class DepositoryCategory
    {
        public int Id { get; set; }

        [DisplayName("نام انبار")]
        [Required(ErrorMessage = "نام انبار را وارد کنید")]
        public string Name { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }
        /// <summary>
        /// If The customer desires to control identiy/uniqu code by himself
        /// </summary>
        [DisplayName("کد انبار")]
        public string Code { get; set; }

        public DepositoryCategory ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
