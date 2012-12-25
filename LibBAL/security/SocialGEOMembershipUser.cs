using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using LibModels;

namespace LibBAL.security
{
    public class SocialGEOMembershipUser : MembershipUser
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public SocialGEOMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate, User user)
            : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            User = user;
        }
    }
}