using System;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using PL.MVC.Models.Like;

namespace PL.MVC.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly IUserService _userService ;
        private readonly ILikeService _likeService;

        public LikeController(IUserService userService, ILikeService likeService)
        {
            _likeService = likeService;
            _userService = userService;
        }

        public ActionResult Create(Guid id)
        {
            try
            {
                var like = new CreateLikeModel
                {
                    PostId = id,
                    UserId = GetUserId()
                };

                var bllLike = Mapper.Map<BllLike>(like);
                bllLike.CreateDate = DateTime.Now;
                _likeService.Create(bllLike);
            }
            catch (Exception)
            {

            }
            return PartialView("_DeleteLike", id);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                _likeService.Delete(id, GetUserId());
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return PartialView("_CreateLike", id);
        }


        [ChildActionOnly]
        public ActionResult Like(Guid id)
        {
            return PartialView("_CreateLike", id);
        }

        [ChildActionOnly]
        public ActionResult UnLike(Guid id)
        {
            return PartialView("_DeleteLike", id);
        }

        private Guid GetUserId()
        {
            return _userService.GetByUserName(HttpContext.User.Identity.Name).Id;
        }

    }
}
