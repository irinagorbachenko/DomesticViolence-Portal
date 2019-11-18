

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DomesticViolencePortal.DAL;
using DomesticViolencePortal.Models;

namespace DomesticViolencePortal.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { RoleName = roleName };
            DomesticViolenceContext db = new DomesticViolenceContext();
            db.Roles.Add(newRole);
            db.SaveChanges();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (DomesticViolenceContext db = new DomesticViolenceContext())
            {
                try
                {
                    // Получаем пользователя

                    User user = db.Users.FirstOrDefault(u => u.Login.UserLogin == username);

                    /*User user = (from u in db.Users
                                 from l in db.Logins
                                 where l.UserLogin == username && u.Login == l
                                 select u).FirstOrDefault();*/

                    //User user = (from u in db.Users
                    //             where u.Login == username
                    //             select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        //Role userRole = db.Roles.Find(user.RoleId);

                        //if (userRole != null)
                        if (user.Role != null)
                        {
                            roles = new string[] { user.Role.RoleName };
                        }
                    }
                }

                catch
                {
                    roles = new string[] { };
                }
            }
            return roles;
        }

        public override bool IsUserInRole(string username, string roleName)
        {

            bool outputResult = false;
            // Находим пользователя
            using (DomesticViolenceContext db = new DomesticViolenceContext())
            {
                try
                {
                    // Получаем пользователя

                    User user = db.Users.FirstOrDefault(u => u.Login.UserLogin == username);

                    /*User user = (from u in db.Users
                                 from l in db.Logins
                                 where l.UserLogin == username && u.Login == l
                                 select u).FirstOrDefault();*/

                    //User user = (from u in db.Users
                    //             where u.Login == username
                    //             select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        //Role userRole = db.Roles.Find(user.RoleId);

                        //сравниваем
                        //if (userRole != null && userRole.RoleName == roleName)
                        if (user.Role != null && user.Role.RoleName == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}