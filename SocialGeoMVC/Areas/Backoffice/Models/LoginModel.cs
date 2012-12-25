using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialGeoMVC.Areas.Backoffice.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(32, ErrorMessage = "De {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(128, ErrorMessage = "De {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}