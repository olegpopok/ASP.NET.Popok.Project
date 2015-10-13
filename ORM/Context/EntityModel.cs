using System.Data.Entity;
using ORM.Entities;


namespace ORM.Context
{
    public class EntityModel : DbContext
    {
        public EntityModel(string connectionString)
            : base(connectionString){ }

        public virtual DbSet<User> Users { get; set; }

        public  virtual DbSet<Avatar> UserAvatars { get; set; }

        public virtual DbSet<PostPhoto> PostPhotos { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avatar>()
                .HasRequired(avatar => avatar.User)
                .WithOptional(user => user.Avatar);

            modelBuilder.Entity<PostPhoto>()
                .HasRequired(photo => photo.Post)
                .WithOptional(post => post.Photo);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Posts)
                .WithRequired(post => post.Author);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Likes)
                .WithRequired(like => like.User)
                .WillCascadeOnDelete(false);

                

            base.OnModelCreating(modelBuilder);
        }
    }
}
