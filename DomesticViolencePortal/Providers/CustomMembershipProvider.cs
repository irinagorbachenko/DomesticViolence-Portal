using DomesticViolencePortal.DAL;
using DomesticViolencePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DomesticViolencePortal.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            using (DomesticViolenceContext db = new DomesticViolenceContext())
            {
                try
                {
                    Models.User user = (from u in db.Users
                                 from l in db.Logins
                                 where l.UserLogin == username && u.UserId == l.Id
                                 select u
                               ).FirstOrDefault();



                    //User user = db.Users.AsEnumerable().FirstOrDefault(u => u.Login.UserLogin == username);

                    if (user != null &&
                        db.Logins.First(l=>l.Id==user.UserId).Password.SequenceEqual(
                            new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Unicode.GetBytes(password))))
                    {
                        isValid = true;
                    }                    
                }
                catch
                {
                    isValid = false;
                }
                return isValid;
            }
        }

        public MembershipUser CreateUser(string lastname, string firstname, string login, byte[] password,string Role )
        {
            MembershipUser membershipUser = GetUser(login, false);
            if (membershipUser == null)
            {
                try
                 {
                    using (DomesticViolenceContext db = new DomesticViolenceContext())
                    {
                        db.Logins.Add(
                            new Models.Login
                            {
                                UserLogin = login,
                                Password = password
                            });
                        db.SaveChanges();

                        User user = new User
                        {
                            LastName = lastname,
                            FirstName = firstname,
                            LoginId = db.Logins.AsEnumerable().Last().Id,
                            RoleId = 2
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        return GetUser(login, false);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }


        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            try
            {
                using (DomesticViolenceContext db = new DomesticViolenceContext())
                {
                    Login login = db.Logins.FirstOrDefault(l => l.UserLogin == username);

                    //Login login = (from l in db.Logins
                    //               where l.UserLogin == username
                    //               select l).FirstOrDefault();

                    if (login != null)
                    {
                        return new MembershipUser("CustomMembershipProvider", login.UserLogin, null, null, null, null, false, false, DateTime.Now, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    }
                }
            }
            catch (Exception ex)
            {
                //string rt = ex.Message;
                return null;
            }
            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string UserId, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}