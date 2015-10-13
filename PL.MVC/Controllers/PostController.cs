using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using PL.MVC.Infrastructure;
using PL.MVC.Models.Post;

namespace PL.MVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IService<BllPostPhoto> _photoService;
        private readonly IService<BllPost> _postService;
        private readonly IUserService _userService;

        public PostController(IService<BllPostPhoto> photoService, IService<BllPost> postService, IUserService userService)
        {
            _photoService = photoService;
            _postService = postService;
            _userService = userService;
        }

        public FileContentResult Photo(Guid id)
        {
            var photo = _photoService.GetById(id);
            if (photo != null)
            {
                return File(photo.Photo, photo.PhotoType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostModel newPost)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userService.GetByUserName(HttpContext.User.Identity.Name);
                newPost.AuthorId = currentUser.Id;
                var newBllPost = Mapper.Map<BllPost>(newPost);
                newBllPost.CreateDate = DateTime.Now;
                var newPostId = _postService.Create(newBllPost);
                AddPhoto(newPostId, newPost.Photo);
                return RedirectToActionPermanent("Profile", "Home", new {id = currentUser.UserName});
            }

            return View(newPost);
        }

        public string Delete(Guid id)
        {
            if (CheckUser(id))
            {
                _photoService.Delete(id);
                _postService.Delete(id);
                return "Post deleted is.";
            }
            else
            {
                return "error";
            }
        }

        public ActionResult Change(Guid id)
        {
            if (CheckUser(id))
            {
                var post = _postService.GetById(id);
                return PartialView("_Change", new ChangePostModel {Id = post.Id, Description = post.Description});
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(ChangePostModel post)
        {
            if (ModelState.IsValid)
            {
                var changePost = _postService.GetById(post.Id);
                changePost.Description = post.Description;
                changePost.AuthorId = changePost.Author.Id;
                _postService.Update(changePost);
                return PartialView("_PostRoot", Mapper.Map<PostViewModel>(changePost));
            }

            return PartialView("_Change", post);
        }

        public ActionResult ChangeCancel(Guid id)
        {
            var post = _postService.GetById(id);
            return PartialView("_PostRoot", Mapper.Map<PostViewModel>(post));
        }

        public ActionResult UserList(Guid id, int page = 1)
        {
            var blockInfo = new UserBlockInfo<PostViewModel>
            {
                PageSize = 5,
                PageNumber = page,
                TotalItems = _postService.Count(post => post.AuthorId == id),
                UserId = id
            };
            var items = _postService.GetByPredicate(post => post.AuthorId == id, post => post.CreateDate, blockInfo.Skip,
                blockInfo.PageSize);
            blockInfo.Items = Mapper.Map<IEnumerable<PostViewModel>>(items);
            return PartialView("_UserBlock", blockInfo);
        }

        public ActionResult Main(int page = 1)
        {
            var userId = GetCurrentUserId();
            var blockInfo = new BlockInfo<PostViewModel>
            {
                PageSize = 5,
                PageNumber = page,
                TotalItems = _postService.Count(post => post.AuthorId != userId)
            };
            var items = _postService.GetByPredicate(post => post.AuthorId != userId, post => post.CreateDate,
                blockInfo.Skip, blockInfo.PageSize);
            blockInfo.Items = Mapper.Map<IEnumerable<PostViewModel>>(items);
            return PartialView("_IndexBlock", blockInfo);
        }

        public ActionResult Search(string search, int page = 1)
        {
            var userId = GetCurrentUserId();
            var blockInfo = new SearchBlockInfo<PostViewModel>
            {
                PageSize = 5,
                PageNumber = page,
                SearchString = search,
                TotalItems = _postService.Count(post => post.Description.Contains(search) && post.AuthorId != userId)
            };
            var items = _postService.GetByPredicate(post => post.Description.Contains(search) && post.AuthorId != userId,
                post => post.CreateDate, blockInfo.Skip, blockInfo.PageSize);
            blockInfo.Items = Mapper.Map<IEnumerable<PostViewModel>>(items);
            return PartialView("_SearchBlock", blockInfo);
        }

        private void AddPhoto(Guid id, HttpPostedFileBase image)
        {
            var photo = new BllPostPhoto();
            photo.Id = id;
            photo.Photo = new byte[image.ContentLength];
            image.InputStream.Read(photo.Photo, 0, image.ContentLength);
            photo.PhotoType = image.ContentType;
            _photoService.Create(photo);
        }

        private bool CheckUser(Guid postId)
        {
            var post = _postService.GetById(postId);

            if (post.Author.UserName.Equals(HttpContext.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        private Guid GetCurrentUserId()
        {
            return _userService.GetByUserName(HttpContext.User.Identity.Name).Id;
        }
    }
}
