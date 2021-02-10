using ChessMeters.Core.Enums;
using ChessMeters.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class Flag
    {
        public Flag(FlagEnum id)
        {
            Id = id;
            Name = id.ToString();
            Description = id.GetEnumDescription();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public FlagEnum Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        [MaxLength(100)]
        public virtual string Description { get; set; }

        public static implicit operator Flag(FlagEnum @enum) => new Flag(@enum);

        public static implicit operator FlagEnum(Flag flag) => flag.Id;
    }
}
