using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibModels;
using LibBAL.orm;
using SocialGeoMVC.Areas.Backoffice.Models;

namespace SocialGeoMVC.Areas.Backoffice.Controllers
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
        /* INDEX (GRID OF ARTICLES)
         * ==========================================================================
         */
        public ActionResult Index()
        {
            var model = Adapter.ArticleRepository.GetAll().OrderByDescending(a => a.CreatedDate);
            return View(model.ToList());
        }
        /* CREATE
         * ==========================================================================
         */
        public ActionResult Create()
        {
            var model = new ArticleCategoriesViewModel()
            {
                Categories = new MultiSelectList(Adapter.CategoryRepository.GetAll(), "ID", "Title"),
                Article = new Article()
            };
            return View(model);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCategoriesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = viewModel.Article;
                model.CreatedDate = DateTime.UtcNow;
                var ids = viewModel.SelectedCategoriesIds;
                if (ids != null && ids.Length > 0)
                {
                    var categories = new List<Category>();
                    foreach (var id in ids)
                    {
                        var category = Adapter.CategoryRepository.Single(c => c.ID.Equals(id), null);
                        categories.Add(category);
                    }
                    model.Categories = categories;
                }                
                Adapter.ArticleRepository.Insert(model);
                Adapter.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        /* UPDATE OR EDIT
         * ==========================================================================
         */
        public ActionResult Edit(int id)
        {
            var article = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), null);
            long[] ids = null;
            if(article.Categories != null && article.Categories.Count > 0){
                ids = new long[article.Categories.Count];
                int i = 0;
                foreach (var category in article.Categories)
                {
                    ids[i] = category.ID;
                    i++;
                }
            }
            var model = new ArticleCategoriesViewModel()
            {
                SelectedCategoriesIds = ids,
                Categories = new MultiSelectList(Adapter.CategoryRepository.GetAll(), "ID", "Title"),
                Article = article
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleCategoriesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //GET ORGINAL MODEL
                var modelOrginal = Adapter.ArticleRepository.Single(a => a.ID.Equals(viewModel.Article.ID), null);
                //ADD NEW VALUES
                modelOrginal.ModifiedDate = DateTime.UtcNow;
                modelOrginal.Title = viewModel.Article.Title;
                modelOrginal.Description = viewModel.Article.Description;
                modelOrginal.Body = viewModel.Article.Body;
                var ids = viewModel.SelectedCategoriesIds;
                if (ids != null && ids.Length > 0)
                {
                    if (modelOrginal.Categories != null)
                    {
                        modelOrginal.Categories.Clear();
                    }
                    var categories = new List<Category>();
                    foreach (var id in ids)
                    {
                        var category = Adapter.CategoryRepository.Single(c => c.ID.Equals(id), null);
                        categories.Add(category);
                    }
                    modelOrginal.Categories = categories;
                }                
                Adapter.ArticleRepository.Update(modelOrginal);
                Adapter.Save();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        /* DELETE
         * ==========================================================================
         */
        public ActionResult Delete(int id)
        {
            var model = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), null);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), null);
            Adapter.ArticleRepository.Delete(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
        public ActionResult Publish(int id)
        {
            var model = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), null);
            model.PublishedDate = DateTime.UtcNow;
            Adapter.ArticleRepository.Update(model);
            Adapter.Save();
            return RedirectToAction("Index");
        }
    }
}
