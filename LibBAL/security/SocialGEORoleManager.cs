using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using LibBAL.orm;
using LibModels;
using System.Collections.Specialized;
using System.Data;

namespace LibBAL.security
{
    public class SocialGEORoleManager : RoleProvider
    {
        #region VARIABLES
        private string _applicationName;
        #endregion

        #region PROPERTIES
        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }
        #endregion

        #region UNITOFWORK
        private UnitOfWork _adapter = null;
        protected UnitOfWork Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    _adapter = new UnitOfWork();
                }
                return _adapter;
            }
        }
        #endregion

        #region CUSTOM METHODS
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }
        private string[] ConvertRoleCollectionToStringArray(ICollection<Role> roles)
        {
            string[] rolesArray = new string[roles.Count];
            int i = 0;
            foreach (Role role in roles)
            {
                rolesArray[i] = role.Title;
                i++;
            }
            return rolesArray;
        }
        private string[] ConvertUserCollectionToStringArray(ICollection<User> users)
        {
            string[] usersArray = new string[users.Count];
            int i = 0;
            foreach (User user in users)
            {
                usersArray[i] = user.UserName;
                i++;
            }
            return usersArray;
        }
        #endregion

        #region INITALIZE
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null) throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name)) name = "SocialGEORoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "SocialGEO Role Provider");
            }

            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"],
                          System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        }
        #endregion

        #region OVERRIDE METHODS
        public override bool RoleExists(string roleName)
        {
            try
            {
                return (Adapter.RoleRepository.Find(r => r.Title.Equals(roleName), null) == null) ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] GetAllRoles()
        {
            try
            {
                ICollection<Role> roles = Adapter.RoleRepository.GetAll().ToList();
                //CREATE STRING ARRAY
                return ConvertRoleCollectionToStringArray(roles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                if (RoleExists(roleName))
                {
                    throw new Exception();
                }
                else
                {
                    Adapter.RoleRepository.Insert(new Role
                    {
                        Title = roleName,
                        Description = roleName,
                        CreatedDate = DateTime.UtcNow
                    });
                    //SAVE CHANGES
                    Adapter.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                if (RoleExists(roleName))
                {
                    //FIND ROLE
                    Role role = Adapter.RoleRepository.Single(r => r.Title.Equals(roleName), null);
                    //TRY TO DELETE
                    Adapter.RoleRepository.Delete(role);
                    //SAVE CHANGES
                    Adapter.Save();
                    //SUCCEED
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                User user = Adapter.UserRepository.Single(u => u.UserName.Equals(username), "Roles");
                //CREATE STRING ARRAY
                return ConvertRoleCollectionToStringArray(user.Roles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            try
            {
                Role role = Adapter.RoleRepository.Single(r => r.Title.Equals(roleName), "Users");
                //CREATE STRING ARRAY
                return ConvertUserCollectionToStringArray(role.Users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            try
            {
                Role role = Adapter.RoleRepository.Single(r => r.Title.Equals(roleName), "Users");
                //CREATE STRING ARRAY
                return ConvertUserCollectionToStringArray(role.Users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                string[] users = GetUsersInRole(roleName);
                bool present = false;
                int i = 0;
                while (!present && i < users.Length)
                {
                    if (users[i] == username)
                    {
                        present = true;
                    }
                    i++;
                }
                return present;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                Role role = Adapter.RoleRepository.Single(r => r.Title.Equals(roleName), "Users");
                foreach (string userName in usernames)
                {
                    User user = Adapter.UserRepository.Single(u => u.UserName.Equals(userName), null);
                    try
                    {
                        if (RoleExists(roleName) && !IsUserInRole(userName, roleName))
                        {
                            role.Users.Add(user);
                            Adapter.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string userName in usernames)
            {
                User user = Adapter.UserRepository.Single(u => u.UserName.Equals(userName), "Roles");
                foreach (string roleName in roleNames)
                {
                    try
                    {
                        Role role = (from r in user.Roles
                                     where r.Title == roleName
                                     select r).First();
                        if (role != null)
                        {
                            user.Roles.Remove(role);
                            Adapter.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion
    }
}
