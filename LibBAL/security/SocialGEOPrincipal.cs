using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.Serialization;
using LibBAL.orm;
using System.Web.Security;

namespace LibBAL.security
{
    [Serializable]
    public class SocialGEOPrincipal : MarshalByRefObject, IPrincipal
    {
        #region IPrincipal Members
        private SocialGEOIdentity _identity;
        public IIdentity Identity
        {
            get { return _identity; }
            set { _identity = (SocialGEOIdentity)value; }
        }
        public string[] GetRoles(string userName)
        {
            try
            {
                return Roles.GetRolesForUser(userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public bool IsInRole(string role)
        {
            try
            {
                return Roles.IsUserInRole(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
        #region CONSTRUCTOR
        public SocialGEOPrincipal(SocialGEOIdentity cIndent)
        {
            this.Identity = cIndent;
        }
        #endregion        
    }
}
