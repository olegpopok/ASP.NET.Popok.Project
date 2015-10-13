using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using ORM.Context;
using ORM.Entities;
using DAL.Interface.Repositories;
using DAL.Interface.DTO;
using DAL.Repositories;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Services;

namespace DependencyResolver
{
    public static class ResolverConfiguration
    {
        public static void Configure(this IKernel kernel)
        {
            Database.SetInitializer<EntityModel>( new DropCreateDatabaseIfModelChanges<EntityModel>());
           // Database.SetInitializer<EntityModel>(new DropCreateDatabaseAlways<EntityModel>());

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope().WithConstructorArgument("DefaultConnection");

            //Repositories
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRepository<DalAvatar>>().To<PhotoRepository<DalAvatar, Avatar>>();
            kernel.Bind<IRepository<DalPostPhoto>>().To<PhotoRepository<DalPostPhoto, PostPhoto>>();
            kernel.Bind<IRepository<DalPost>>().To<Repository<DalPost, Post>>();
            kernel.Bind<ILikeRepository>().To<LikeRepository>();

            //Services
            kernel.Bind<IService<BllAvatar>>().To<Service<BllAvatar, DalAvatar>>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IService<BllPostPhoto>>().To<Service<BllPostPhoto, DalPostPhoto>>();
            kernel.Bind<IService<BllPost>>().To<Service<BllPost, DalPost>>();
            kernel.Bind<ILikeService>().To<LikeService>();
        }
    }
}
