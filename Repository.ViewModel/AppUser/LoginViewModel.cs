using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "پست الکترونیکی")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required(ErrorMessage ="نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
