using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Common
{
    public class About
    {
        [DisplayName("شناسه")]
        public int Id { get; set; }

        [Required(ErrorMessage ="نام فروشگاه را وارد کنید")]
        [DisplayName("نام فروشگاه")]
        public string Name { get; set; }

        [Required(ErrorMessage = "آدرس را وارد کنید")]
        [DisplayName("آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        [DisplayName("شماره تماس")]
        public string Tel { get; set; }
    }
}
