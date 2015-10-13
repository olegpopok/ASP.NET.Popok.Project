using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ORM.Entities
{
    public class User : IEntity
    {
        public User() 
        {
            Posts = new HashSet<Post>();
            Likes = new HashSet<Like>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required, MaxLength(32)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Bio { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required, MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        public virtual Avatar Avatar { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Like> Likes { get; set; } 
 
    }
}
