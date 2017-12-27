using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.AppUser
{
    class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
