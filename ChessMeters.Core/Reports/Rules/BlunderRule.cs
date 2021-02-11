using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System;
using System.Linq;

namespace ChessMeters.Core.Reports
{
    public class BlunderRule : IRule
    {
        public bool IsGameRule { get { return false; } }

        public FlagEnum Flag { get { return FlagEnum.Blunder; } }

        public bool Evaluate(IBoardState board)
        {
            // Cannot blunder on ply number 1 of the game.
            if (board.CurrentTreeMove.MoveNumber <= 1)
            {
                return false;
            }

            // Check any sensitive differences on evals.
            var currentEvaluation = GetEvaluationAverage(board.CurrentTreeMove);
            var previousEvaluation = GetEvaluationAverage(board.PreviousTreeMove);

            return Math.Abs(currentEvaluation - previousEvaluation) > 250;
        }

        private static double GetEvaluationAverage(TreeMove treeMove)
        {
            return treeMove.EngineEvaluations.Average(x => x.EvaluationCentipawns);
        }
    }
}