using ChessMeters.Core.Entities;
using System;
using System.Linq;

namespace ChessMeters.Core.Coach
{
    public class CoachRuleBlunder : ICoachRule
    {
        public bool IsGameRule { get { return false; } }

        public FlagEnum? Evaluate(ICoachBoard board)
        {
            // Cannot blunder on ply number 1 of the game.
            if (board.CurrentTreeMove.MoveNumber <= 1)
            {
                return null;
            }

            // Check any sensitive differences on evals.
            var currentEvaluation = GetEvalForPly(board.CurrentTreeMove);
            var previousEvaluation = GetEvalForPly(board.PreviousTreeMove);

            return Math.Abs(currentEvaluation - previousEvaluation) > 250 ? FlagEnum.Blunder : null;
        }

        private int GetEvalForPly(TreeMove treeMove)
        {
            return treeMove.EngineEvaluations.Single(x => x.EngineId == EngineEnum.Stockfish12).EvaluationCentipawns;
        }
    }
}