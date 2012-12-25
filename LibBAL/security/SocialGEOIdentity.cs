using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.Serialization;
using System.Web.Security;
using LibModels;
using LibBAL.orm;

namespace LibBAL.security
{
    [Serializable]
    public class SocialGEOIdentity : MarshalByRefObject, IIdentity
    {
        #region PROPERTIES
        //TICKET
        private FormsAuthenticationTicket _ticket;
        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion

        #region IIDENTITY PROPERTIES
        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return (User.Profile == null) ? User.UserName : (User.Profile.ProfileData.FirstName + " " + User.Profile.ProfileData.SurName); }
        }
        #endregion

        #region CONSTRUCTOR
        public SocialGEOIdentity(FormsAuthenticationTicket ticket)
        {
            Ticket = ticket;
            SetUser();
        }
        private void SetUser()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                User = unitOfWork.UserRepository.Single(u => u.UserName.Equals(Ticket.Name), "Profile");
                if (User == null)
                {
                    User = unitOfWork.UserRepository.Single(u => u.UserName.Equals(Ticket.Name), null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
