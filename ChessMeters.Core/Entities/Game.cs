using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Moves { get; set; }

        [Required]
        [MaxLength(7)]
        public string Result { get; set; }
    }
}
