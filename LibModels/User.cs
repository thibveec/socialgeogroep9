using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LibModels
{
    public class User
    {
        #region PROPERTIES
        public Int64 ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        public string ThumbnailUrl { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string ActivationKey { get; set; }
        public Nullable<DateTime> ActivatedDate { get; set; }
        public Nullable<DateTime> LockedDate { get; set; }
        public Nullable<DateTime> LastLoggedInDate { get; set; }
        public Nullable<DateTime> LastActivityDate { get; set; }
        public Nullable<DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<int> AmountOfLoggedIn { get; set; }
        #endregion

        #region COMPLEX PROPERTIES
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        #endregion
    }
}
