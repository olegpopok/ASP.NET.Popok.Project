using AutoMapper;
using DAL.Interface.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public  static class BllEntityMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<BllUser, DalUser>();
            Mapper.CreateMap<DalUser, BllUser>();

            Mapper.CreateMap<DalLike, BllLike>();
            Mapper.CreateMap<BllLike, DalLike>();

            Mapper.CreateMap<BllAvatar, DalAvatar>();
            Mapper.CreateMap<DalAvatar, BllAvatar>();

            Mapper.CreateMap<BllPost, DalPost>()
               .ForMember(post => post.Author, opt => opt.Ignore())
               .ForMember(dalPost => dalPost.Likes, opt => opt.Ignore());
            Mapper.CreateMap<DalPost, BllPost>()
                .ForMember(dalPost => dalPost.AuthorId, opt => opt.Ignore())
                .ForMember(bllPost => bllPost.Likes, opt => opt.MapFrom(dalLike => dalLike.Likes));

            Mapper.CreateMap<DalPostPhoto, BllPostPhoto>();
            Mapper.CreateMap<BllPostPhoto, DalPostPhoto>();

            
        }
    }
}
