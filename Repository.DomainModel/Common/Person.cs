using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Common
{
    public class Person
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "نام را وارد کنید")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
    }
}
