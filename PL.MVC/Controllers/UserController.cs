using System;
using System.IO;
using System.Web.Mvc;
using BLL.Interface.Services;
using BLL.Interface.Entities;

namespace PL.MVC.Controllers
{
    public class UserController : Controller
    {
        private const string DefaultAvatarPath = "~/Content/images/default-avatar.jpg";
        private const string DefaultAvatarType = "image/jpeg";

        private readonly IUserService _userService;
        private readonly IService<BllAvatar> _avatarService;
 
        public UserController (IUserService userService, IService<BllAvatar> avatarServise)
        {
            _userService = userService;
            _avatarService = avatarServise;
        }

        public FileContentResult Avatar(Guid id)
        {
            var avatar = _avatarService.GetById(id);
            if (avatar != null)
            {
                return File(avatar.Image, avatar.MineType);
            }
            else
            {
                string path = HttpContext.Server.MapPath(DefaultAvatarPath);
                if (System.IO.File.Exists(path))
                {
                    using(var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
                    {
                        var image = reader.ReadBytes((int)reader.BaseStream.Length);
                        return File(image, DefaultAvatarType);
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
