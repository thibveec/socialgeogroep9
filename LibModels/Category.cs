using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModels
{
    public class Category : Item
    {
        public Nullable<Int64> ParentCategoryID {get; set;}

        //COMPLEX TYPES
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
