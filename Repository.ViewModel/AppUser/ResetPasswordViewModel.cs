using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "این {0} باید حداقل {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تائیدیه رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور جدید و تائیدیه یکسان نیستند.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
