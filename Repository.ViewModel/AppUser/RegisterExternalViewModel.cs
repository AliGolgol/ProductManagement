using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class RegisterExternalViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
