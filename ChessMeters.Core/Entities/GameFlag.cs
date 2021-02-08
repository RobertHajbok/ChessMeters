using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class GameFlag
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Report { get; set; }

        public FlagEnum FlagId { get; set; }

        [ForeignKey(nameof(FlagId))]
        public virtual Flag Flag { get; set; }
    }
}
