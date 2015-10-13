using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ORM.Entities
{
    [Table("UserAvatars")]
    public class Avatar : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public  Guid Id { get; set; }
        
        [Required, MaxLength(50)]
        public string MineType { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public virtual User User { get; set; }
    }
}
