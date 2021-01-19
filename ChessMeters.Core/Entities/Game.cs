using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int ReportId { get; set; }

        [ForeignKey(nameof(ReportId))]
        public virtual Report Report { get; set; }

    }
}
