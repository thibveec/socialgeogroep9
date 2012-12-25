using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibBAL.orm;
using LibModels;

namespace SocialGeoMVC.Api
{
    public class ArticleController : ApiController
    {
        #region UNITOFWORK
        private UnitOfWork _adapter = null;
        protected UnitOfWork Adapter
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
        #endregion

        // GET api/article
        public IEnumerable<Article> Get()
        {
            return Adapter.ArticleRepository.GetAll("Categories,Comments").OrderByDescending(a => a.CreatedDate);
        }

        // GET api/article/5
        public Article Get(int id)
        {
            return Adapter.ArticleRepository.Single(a => a.ID.Equals(id), "Categories,Comments");
        }

        // POST api/article
        public void Post([FromBody]string value)
        {
        }

        // PUT api/article/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/article/5
        public void Delete(int id)
        {
            var model = Adapter.ArticleRepository.Single(a => a.ID.Equals(id), null);
            Adapter.ArticleRepository.Delete(model);
            Adapter.Save();
        }
    }
}
