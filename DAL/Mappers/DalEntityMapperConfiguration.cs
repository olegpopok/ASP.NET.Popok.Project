using System.Linq;
using AutoMapper;
using ORM.Entities;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class DalEntityMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, DalUser>()
                .ForMember(dalUser => dalUser.PostCount, opt => opt.MapFrom(user => user.Posts.Count));
            Mapper.CreateMap<DalUser, User>();

            Mapper.CreateMap<DalAvatar, Avatar>();
            Mapper.CreateMap<Avatar, DalAvatar>();

            Mapper.CreateMap<Like, DalLike>()
                .ForMember(likeDto => likeDto.UserName, opt => opt.MapFrom(like => like.User.UserName));
            Mapper.CreateMap<DalLike, Like>().ForMember(like => like.User, opt => opt.Ignore());

            Mapper.CreateMap<DalPost, Post>()
                .ForMember(post => post.Author, opt => opt.Ignore())
                .ForMember(post =>post.Likes, opt => opt.Ignore());
            Mapper.CreateMap<Post, DalPost>()
                .ForMember(dalPost => dalPost.Likes, opt => opt.MapFrom(post => post.Likes.ToList()));

            Mapper.CreateMap<DalPostPhoto, PostPhoto>();
            Mapper.CreateMap<PostPhoto, DalPostPhoto>();
        }

    }
}
