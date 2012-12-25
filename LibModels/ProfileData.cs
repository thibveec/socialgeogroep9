using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LibModels
{
    public class ProfileData
    {
        #region PROPERTIES
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string FullName { get { return FirstName + ' ' + SurName; } }
        public Nullable<DateTime> DayOfBirth { get; set; }  
        #endregion
    }
}
