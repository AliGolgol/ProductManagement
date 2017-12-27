using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }
    }
}
