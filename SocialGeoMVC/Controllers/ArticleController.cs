using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibBAL.orm;

namespace SocialGeoMVC.Controllers
{
    public class ArticleController : Controller
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(Int64 id)
        {
            //RETURN VIEWDATA FOR PARTIAL VIEWS
            ViewData["ArticleId"] = id;
            return View();
        }
        #region PARTIALVIEWS
        [ChildActionOnly]
        public ActionResult PVListMediumAll()
        {
            var model = Adapter.ArticleRepository.GetAll();
            return PartialView("_ArticleListMedium", model.ToList());
        }
        [ChildActionOnly]
        public ActionResult PVListMediumAmount(int amount)
        {
            var model = Adapter.ArticleRepository.GetAll().Take(amount);
            return PartialView("_ArticleListMedium", model.ToList());
        }
        [ChildActionOnly]
        public ActionResult PVDetails(Int64 id)
        {
            var model = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), "Categories");
            return PartialView("_ArticleDetails", model);
        }
        #endregion
    }
}
