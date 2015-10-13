using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using PL.MVC.Models.Account;
using PL.MVC.Providers;

namespace PL.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IService<BllAvatar> _avatarService;

        public AccountController(IUserService userService, IService<BllAvatar> avatarService)
        {
            _userService = userService;
            _avatarService = avatarService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.Remember);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password is incorrect");
            }
            
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel newUser)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByUserName(newUser.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "The user name you entered already exists");
                    return View(newUser);
                }

                if (newUser.Password.Contains(newUser.UserName))
                {
                    ModelState.AddModelError("", "User name and password can't be equal.");
                    return View(newUser);
                }

                var membershipUser = ((UserMembershipProvider)Membership.Provider).CreateUser(newUser);
                if (membershipUser != null)
                {
                    if (newUser.Avatar != null)
                    {
                        SetUserAvatar((Guid)membershipUser.ProviderUserKey, newUser.Avatar);
                    }
                    FormsAuthentication.SetAuthCookie(newUser.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Eror registration.");
                    return View(newUser);
                }
            }
            return View(newUser);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(LocalPasswordModel model)
        {
            var userName = HttpContext.User.Identity.Name;
            if ( ModelState.IsValid && ((UserMembershipProvider) Membership.Provider).ChangePassword(userName, model.OldPassword,
                model.NewPassword))
            {
                ViewBag.Message = "Password is changed successfully.";
                return View(model);
            }

            ModelState.AddModelError("OldPassword", "Password is incorrect");
            return View(model);
        }

        public ActionResult EditProfile()
        {
            var currentUser = _userService.GetByUserName(HttpContext.User.Identity.Name);
            return View(Mapper.Map<EditProfileModel>(currentUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByUserName(model.UserName);
                if (user != null && model.UserName != HttpContext.User.Identity.Name)
                {
                    ModelState.AddModelError("UserName", "The user name you entered already exists.");
                    return View(model);
                }

                user = _userService.GetByUserName(HttpContext.User.Identity.Name);
                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Bio = model.Bio;
                user.Website = model.Website;
                _userService.Update(user);

                if (model.UserName != HttpContext.User.Identity.Name)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    string[] roles = new string[] { "User"};

                    HttpContext.User =
                       new GenericPrincipal(new GenericIdentity(model.UserName), roles);
                }
                ViewBag.Message = "Profile is changed successfully.";
                return View(model);
            }
            return View(model);
        }

        private void SetUserAvatar(Guid userId, HttpPostedFileBase image)
        {
            var avatar = new BllAvatar();
            avatar.Id = userId;
            avatar.Image = new byte[image.ContentLength];
            image.InputStream.Read(avatar.Image, 0, image.ContentLength);
            avatar.MineType = image.ContentType;
            _avatarService.Create(avatar);
        }
    }
}
