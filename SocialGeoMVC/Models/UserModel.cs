using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialGeoMVC.Models
{
    public class UserModel
    {
        #region PROPERTIES
        public Int64 ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [StringLength(12, ErrorMessage = "The {0} must have at least {2} characters.", MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(64, ErrorMessage = "The {0} must have at least {2} characters.", MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid {0}")]
        [Display(Name = "Email address")]
        [StringLength(128, ErrorMessage = "The {0} must have at least {2} characters.", MinimumLength = 6)]
        public string Email { get; set; }
        #endregion
    }
}