using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class TreeMoveFlag
    {
        public int Id { get; set; }

        public long TreeMoveId { get; set; }

        [ForeignKey(nameof(TreeMoveId))]
        public virtual TreeMove TreeMove { get; set; }

        public FlagEnum FlagId { get; set; }

        [ForeignKey(nameof(FlagId))]
        public virtual Flag Flag { get; set; }
    }
}
