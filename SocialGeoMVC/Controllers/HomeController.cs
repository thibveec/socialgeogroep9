using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibBAL.orm;
using LibModels;
using System.Web.Security;
using LibBAL.security;
using Newtonsoft.Json;

namespace SocialGeoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";      
            return View();
        }
    }
}
