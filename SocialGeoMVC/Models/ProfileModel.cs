using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialGeoMVC.Models
{
    public class ProfileModel
    {
        #region PROPERTIES
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Firstname")]
        [StringLength(128, ErrorMessage = "The {0} must have at least {2} characters.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [StringLength(256, ErrorMessage = "The {0} must have at least {2} characters.", MinimumLength = 2)]
        public string SurName { get; set; }
        #endregion
    }
}