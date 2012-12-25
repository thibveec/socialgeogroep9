using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LibModels
{
    public class Profile
    {
        #region VARIABLES
        private string _json;
        #endregion

        #region PROPERTIES
        [Required]
        public Int64 UserId { get; set; }
        public string JSON
        {
            get { _json = JsonConvert.SerializeObject(ProfileData, Formatting.None); return _json; }
            set { _json = value; ProfileData = JsonConvert.DeserializeObject<ProfileData>(_json); }
        }
        [Required]
        public ProfileData ProfileData { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }        
        #endregion

        #region COMPLEX PROPERTIES 
        [Required]
        public virtual User User { get; set; }
        #endregion
    }
}
