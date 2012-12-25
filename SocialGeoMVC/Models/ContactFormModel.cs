using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;

namespace SocialGeoMVC.Models
{
    public class ContactFormModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Naam")]
        [StringLength(64, ErrorMessage = "De {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [Email]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Onderwerp")]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Bericht")]
        [StringLength(500, ErrorMessage = "Het {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 6)]
        public string Body { get; set; }
    }    
}
