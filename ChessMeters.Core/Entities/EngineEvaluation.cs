using ChessMeters.Core.Engines.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class EngineEvaluation
    {
        public long Id { get; set; }

        public long TreeMoveId { get; set; }

        public virtual TreeMove TreeMove { get; set; }

        public EngineEnum EngineId { get; set; }

        [ForeignKey(nameof(EngineId))]
        public virtual Engine Engine { get; set; }

        public short Depth { get; set; }

        public short EvaluationCentipawns { get; set; }
    }
}
