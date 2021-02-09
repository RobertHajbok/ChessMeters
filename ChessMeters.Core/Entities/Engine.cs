using ChessMeters.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class Engine
    {
        public Engine(EngineEnum id)
        {
            Id = id;
            Name = id.ToString();
            Description = id.GetEnumDescription();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EngineEnum Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        [MaxLength(100)]
        public virtual string Description { get; set; }

        public static implicit operator Engine(EngineEnum @enum) => new Engine(@enum);

        public static implicit operator EngineEnum(Engine engine) => engine.Id;
    }
}
