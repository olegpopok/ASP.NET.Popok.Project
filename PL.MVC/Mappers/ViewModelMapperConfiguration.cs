using System;
using PL.MVC.Models.User;
using PL.MVC.Models.Account;
using BLL.Interface.Entities;
using  AutoMapper;
using PL.MVC.Models.Like;
using PL.MVC.Models.Post;

namespace PL.MVC.Mappers
{
    public class ViewModelMapperConfiguration
    {
        public static void Configure()
        {
            //User
            Mapper.CreateMap<RegisterModel, BllUser>();
            Mapper.CreateMap<BllUser, UserViewModel>();
            Mapper.CreateMap<BllUser, EditProfileModel>();

            //Like
            Mapper.CreateMap<BllLike, ViewLikeModel>();
            Mapper.CreateMap<CreateLikeModel, BllLike>();

            //Post
            Mapper.CreateMap<CreatePostModel, BllPost>();
            Mapper.CreateMap<BllPost, PostViewModel>();
        }
    }
}