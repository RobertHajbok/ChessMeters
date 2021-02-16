using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System;
using System.Linq;

namespace ChessMeters.Core.Reports
{
    public class MistakeRule : IRule
    {
        public bool IsGameRule { get { return false; } }

        public FlagEnum Flag { get { return FlagEnum.Mistake; } }

        public bool Evaluate(IBoardState board)
        {
            if (board.CurrentTreeMove.MoveNumber <= 1)
            {
                return false;
            }

            // Check any sensitive differences on evals.
            var currentEvaluation = GetEvaluationAverage(board.CurrentTreeMove);
            var previousEvaluation = GetEvaluationAverage(board.PreviousTreeMove);

            return Math.Abs(currentEvaluation - previousEvaluation) > 100;
        }

        private static double GetEvaluationAverage(TreeMove treeMove)
        {
            return treeMove.EngineEvaluations.Average(x => x.EvaluationCentipawns);
        }
    }
}