using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SocialGeoMVC.Areas.Backoffice.Models;
using LibBAL.orm;

namespace SocialGeoMVC.Areas.Backoffice.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _adapter = null;
        public UnitOfWork Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    _adapter = new UnitOfWork();
                }
                return _adapter;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        /* INDEX (GRID OF USERS)
         * ==========================================================================
         */
        public ActionResult Index()
        {
            var model = Adapter.UserRepository.GetAll();
            return View(model.ToList());
        }
        /* DELETE
         * ==========================================================================
         */
        public ActionResult Delete(int id)
        {
            var model = Adapter.UserRepository.Single(a => a.ID.Equals(id), null);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = Adapter.UserRepository.Single(a => a.ID.Equals(id), null);
            Adapter.UserRepository.Delete(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
        /* LOCK AND UNLOCK
         * ==========================================================================
         */
        public ActionResult Lock(int id)
        {
            var model = Adapter.UserRepository.Single(a => a.ID.Equals(id), null);
            model.LockedDate = DateTime.UtcNow;
            Adapter.UserRepository.Update(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
        public ActionResult UnLock(int id)
        {
            var model = Adapter.UserRepository.Single(a => a.ID.Equals(id), null);
            model.LockedDate = null;
            Adapter.UserRepository.Update(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
    }
}
