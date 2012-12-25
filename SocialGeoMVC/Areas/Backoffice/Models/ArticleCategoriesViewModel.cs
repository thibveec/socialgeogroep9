using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibModels;
using System.Web.Mvc;

namespace SocialGeoMVC.Areas.Backoffice.Models
{
    public class ArticleCategoriesViewModel
    {
        public long[] SelectedCategoriesIds { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [AllowHtml] 
        public Article Article { get; set; }
    }
}