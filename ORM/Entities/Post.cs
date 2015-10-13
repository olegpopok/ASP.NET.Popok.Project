using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class Post : IEntity
    {
        public Post()
        {
            Likes = new HashSet<Like>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual PostPhoto Photo { get; set; }

        public virtual ICollection<Like> Likes { get; set; }  
    }
}
