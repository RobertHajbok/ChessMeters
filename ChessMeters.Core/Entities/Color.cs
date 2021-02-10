using ChessMeters.Core.Enums;
using ChessMeters.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class Color
    {
        public Color(ColorEnum id)
        {
            Id = id;
            Name = id.ToString();
            Description = id.GetEnumDescription();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ColorEnum Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        [MaxLength(100)]
        public virtual string Description { get; set; }

        public static implicit operator Color(ColorEnum @enum) => new Color(@enum);

        public static implicit operator ColorEnum(Color color) => color.Id;
    }
}
