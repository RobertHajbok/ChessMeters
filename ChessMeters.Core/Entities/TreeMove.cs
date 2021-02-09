using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class TreeMove
    {
        public long Id { get; set; }

        [MaxLength(5)]
        public string Move { get; set; }

        public ColorEnum ColorId { get; set; }

        public long? ParentTreeMoveId { get; set; }

        [ForeignKey(nameof(ParentTreeMoveId))]
        public virtual TreeMove ParentTreeMove { get; set; }

        public string FullPath { get; set; }

        [NotMapped]
        public int MoveNumber
        {
            get { return FullPath?.Split(' ').Length ?? 0; }
        }

        public virtual ICollection<EngineEvaluation> EngineEvaluations { get; set; }

        public virtual ICollection<TreeMoveFlag> TreeMoveFlags { get; set; }
    }
}
