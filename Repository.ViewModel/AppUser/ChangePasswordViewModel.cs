using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.AppUser
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="رمز جدید را وارد کنید")]
        [StringLength(100, ErrorMessage = "این {0} باید حداقل {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تائیدیه رمز عبور جدید")]
        [DataType(DataType.Password)]
        [Display(Name = "تائیدیه رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تائیدیه یکسان نیستند")]
        public string ConfirmPassword { get; set; }
    }
}
