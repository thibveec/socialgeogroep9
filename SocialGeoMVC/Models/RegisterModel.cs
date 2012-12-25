using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibModels;

namespace SocialGeoMVC.Models
{
    public class RegisterModel
    {
        public UserModel UserModel { get; set; }
        public ProfileModel ProfileModel { get; set; }
    }
}