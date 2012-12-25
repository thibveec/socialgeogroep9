using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibBAL.orm;
using LibModels;

namespace SocialGeoMVC.Areas.Backoffice.Controllers
{
    public class RoleController : Controller
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
        /* INDEX (GRID OF ROLES)
         * ==========================================================================
         */
        public ActionResult Index()
        {
            var model = Adapter.RoleRepository.GetAll();
            return View(model.ToList());
        }
        /* CREATE
         * ==========================================================================
         */
        public ActionResult Create()
        {
            return View(new Role());
        }
        [HttpPost]
        public ActionResult Create(Role model)
        {
            if (ModelState.IsValid)
            {
                Adapter.RoleRepository.Insert(model);
                model.CreatedDate = DateTime.UtcNow;
                Adapter.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /* UPDATE OR EDIT
         * ==========================================================================
         */
        public ActionResult Edit(int id)
        {
            var model = Adapter.RoleRepository.Single(a => a.ID.Equals(id), null);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Role model)
        {
            if (ModelState.IsValid)
            {
                //GET ORGINAL MODEL
                var modelOrginal = Adapter.RoleRepository.Single(a => a.ID.Equals(model.ID), null);
                //ADD NEW VALUES
                modelOrginal.ModifiedDate = DateTime.UtcNow;
                modelOrginal.Title = model.Title;
                modelOrginal.Description = model.Description;
                Adapter.RoleRepository.Update(modelOrginal);
                Adapter.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /* DELETE
         * ==========================================================================
         */
        public ActionResult Delete(int id)
        {
            var model = Adapter.RoleRepository.Single(a => a.ID.Equals(id), null);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = Adapter.RoleRepository.Single(a => a.ID.Equals(id), null);
            Adapter.RoleRepository.Delete(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
    }
}
