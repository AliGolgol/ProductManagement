using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.AppUser
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }
    }
}
