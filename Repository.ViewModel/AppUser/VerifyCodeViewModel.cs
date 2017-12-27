using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = " این مرورگر را بخاطر بسپار?")]
        public bool RememberBrowser { get; set; }
    }
}
