using System;
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

        public int ReportId { get; set; }

        [ForeignKey(nameof(ReportId))]
        public virtual Report Report { get; set; }

        public long? LastTreeMoveId { get; set; }

        [ForeignKey(nameof(LastTreeMoveId))]
        public virtual TreeMove LastTreeMove { get; set; }

        [MaxLength(100)]
        public string Event { get; set; }

        [MaxLength(100)]
        public string Site { get; set; }

        [MaxLength(50)]
        public string Round { get; set; }

        [MaxLength(100)]
        public string White { get; set; }
        [MaxLength(100)]
        public string Black { get; set; }

        public short? WhiteElo { get; set; }

        public short? BlackElo { get; set; }

        [MaxLength(3)]
        public string Eco { get; set; }

        [MaxLength(100)]
        public string TimeControl { get; set; }

        [MaxLength(100)]
        public string Termination { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? EndTime { get; set; }

        public DateTime? UTCDate { get; set; }

        public TimeSpan? UTCTime { get; set; }

        [MaxLength(5)]
        public string WhiteRatingDiff { get; set; }

        [MaxLength(5)]
        public string BlackRatingDiff { get; set; }

        [MaxLength(30)]
        public string Variant { get; set; }
    }
}
