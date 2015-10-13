using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    [Table("PostPhotos")]
    public class PostPhoto : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [MaxLength(50), Required]
        public string PhotoType { get; set; }

        public virtual Post Post { get; set; }
    }
}
