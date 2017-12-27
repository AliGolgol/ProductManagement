using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "این {0} باید حداقل {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تائیدیه رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تائیدیه یکسان نیستند.")]
        public string ConfirmPassword { get; set; }
    }
}
