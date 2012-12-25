using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModels
{
    public class Role : Item
    {
        #region COMPLEX PROPERTIES
        public virtual ICollection<User> Users { get; set; }
        #endregion
    }
}