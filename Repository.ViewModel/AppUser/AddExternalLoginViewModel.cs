using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class AddExternalLoginViewModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}
