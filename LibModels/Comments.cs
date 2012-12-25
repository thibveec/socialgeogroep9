using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModels
{
    public class Comment : Item
    {
        public string Body { get; set; }
        public Nullable<Int64> ParentCommentID { get; set; }
        public Nullable<Int64> ArticleID { get; set; }
        

        //COMPLEX TYPES
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> SubComments { get; set; }
        public virtual Article Article { get; set; }
        
    }
}
