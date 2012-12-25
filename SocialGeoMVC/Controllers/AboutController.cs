using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialGeoMVC.Models;
using System.Collections.Specialized;
using LibBAL.mail;
using System.Net.Mail;

namespace SocialGeoMVC.Controllers
{
    public class AboutController : Controller
    {
        /* THE ABOUT PAGE */
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        /* TERMS OF USAGE */
        [AllowAnonymous]
        public ActionResult Usage()
        {
            return View();
        }
        /* CONTACT FORM */
        [AllowAnonymous]
        public ActionResult Contact()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-Selecteer een optie-", Value = "Selecteer subject" });
            list.Add(new SelectListItem { Text = "Aanvraag opleidingsbrochure", Value = "Aanvraag opleidingsbrochure" });
            list.Add(new SelectListItem { Text = "Aanvraag Arteveldemagazine", Value = "Aanvraag Arteveldemagazine" });
            list.Add(new SelectListItem { Text = "Opmerkingen en/of suggesties", Value = "Opmerkingen en/of suggesties" });
            list.Add(new SelectListItem { Text = "Andere vragen", Value = "Andere vragen" });
            ViewData["Subjects"] = list;
            return View(new ContactFormModel());
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendContactForm(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                //SENDING EMAIL
                try
                {
                    //1. GET EMAIL TO INFORMATION
                    /*string emailToName = "Philippe De Pauw - Waterschoot";
                    string emailTo = "philippe.depauw@arteveldehs.be";

                    //2. GET TEMPLATES FOR CONTACT MAIL
                    //2.1. PLAIN TEMPLATE (NO HTML MAIL FOR CONTACT THINS
                    string physicalPath = Server.MapPath("~/Templates/Mail/ContactPlain.txt");
                    string fileContent = System.IO.File.ReadAllText(physicalPath);
                    ListDictionary dictionary = new ListDictionary();
                    dictionary.Add("<%FROMNAME%>", model.Name);
                    dictionary.Add("<%FROMEMAIL%>", model.Email);
                    dictionary.Add("<%SUBJECT%>", model.Subject);
                    dictionary.Add("<%BODY%>", model.Body);
                    dictionary.Add("<%TONAME%>", emailToName);
                    foreach (string key in dictionary.Keys)
                    {
                        fileContent = fileContent.Replace(key, (string)dictionary[key]);
                    }

                    //3. SETUP MAILMANAGER
                    MailManager mailManager = new MailManager();
                    #region GMAIL
                    mailManager.SmtpHost = "smtp.gmail.com";
                    mailManager.SmtpPort = 587;
                    mailManager.IsSSL = true;
                    mailManager.SmtpLogin = "";
                    mailManager.SmtpPassword = "";
                    #endregion
                    mailManager.EmailFrom = new MailAddress(model.Email, model.Name);
                    mailManager.EmailTos.Add(new MailAddress(emailTo, emailToName));
                    mailManager.EmailSubject = model.Subject;
                    mailManager.EmailBodyPlain = fileContent;
                    mailManager.SendMail();*/

                    return Json(new JsonBack{Result = 2, Message = "Uw aanvraag werd verstuurd!"});
                }
                catch (Exception e)
                {
                    return Json(new JsonBack { Result = 1, Message = e.ToString() });
                }
            }
            return Json(new JsonBack{Result = 0, Message = "Niet alle ingevulde velden zijn valid"});
        }

        public class JsonBack
        {
            public int Result { get; set; }
            public string Message { get; set; }
        }
    }
}
