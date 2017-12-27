using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel.AppUser
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "نام نقش")]
        public string Name { get; set; }
    }
}
