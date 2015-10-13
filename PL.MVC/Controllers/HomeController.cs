using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using PL.MVC.Infrastructure;
using PL.MVC.Models.User;

namespace PL.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IService<BllPost> _postService;
 
        public HomeController(IUserService userService, IService<BllPost> postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Profile(string id)
        {
            var bllUser = _userService.GetByUserName(id);
            var viewUser = Mapper.Map<UserViewModel>(bllUser);            
            return View("UserProfile", viewUser);
        }

        public ActionResult Search(string search)
        {
            var userId = _userService.GetByUserName(HttpContext.User.Identity.Name).Id;
            ViewBag.Count = _postService.Count(post => post.Description.Contains(search) && post.AuthorId != userId);
            return View((object)search);
        }
        
    }
}
