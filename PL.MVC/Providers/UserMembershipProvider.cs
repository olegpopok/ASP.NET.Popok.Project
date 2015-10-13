using System.Web.Security;
using BLL.Interface.Services;
using System.Linq;
using System.Web.Helpers;
using PL.MVC.Models.Account;
using System;
using AutoMapper;
using BLL.Interface.Entities;


namespace PL.MVC.Providers
{
    public class UserMembershipProvider : MembershipProvider
    {
        private IUserService userService
        {
            get
            {
                return (IUserService)System.Web.Mvc.DependencyResolver
                .Current.GetService(typeof(IUserService)); 
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = userService.GetByUserName(username);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            else
            {
                return false;   
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = userService.GetByUserName(username);

            if (user == null)
            {
                return null;
            }

            var membershipUser = new MembershipUser("UserMembershipProvider", user.UserName,
                user.Id, null, null, null, false, false, user.CreateDate, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            return membershipUser;
        }

        public MembershipUser CreateUser(RegisterModel newUser)
        {
            var membershipUser = GetUser(newUser.UserName, false);

            if (membershipUser != null)
            {
                return null;
            }
            else
            {
                var bllUser = Mapper.Map<BllUser>(newUser);
                bllUser.CreateDate = DateTime.Now;
                bllUser.Password = Crypto.HashPassword(bllUser.Password);
                userService.Create(bllUser);
                membershipUser = GetUser(newUser.UserName, false);
                return membershipUser;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var bllUser = userService.GetByUserName(username);
            if (bllUser != null && Crypto.VerifyHashedPassword(bllUser.Password, oldPassword))
            {
                bllUser.Password = Crypto.HashPassword(newPassword);
                userService.Update(bllUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Not implemented members

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
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

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
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

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}