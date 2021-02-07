using System;

namespace ChessMeters.Core.Coach
{
    public class CoachRuleBlunder : ICoachRule
    {
        public ICoachFlag? Evaluate(ICoachBoard board)
        {
            // Cannot blunder on ply number 1 of the game.
            if (board.GetCurrentPlyNumber() <= 1)
            {
                return null;
            }

            // Check any sensitive differences on evals.
            var current_eval = GetEvalForPly(board, board.GetCurrentPlyNumber());
            var previous_eval = GetEvalForPly(board, board.GetCurrentPlyNumber() - 1);

            if (Math.Abs(current_eval - previous_eval) > 250)
            {
                return new CoachFlagBlunder(board);
            }
             
            return null;
        }

        private int GetEvalForPly(ICoachBoard board, int ply_number)
        {
            return board.GetStockfishCentipawns().
                GetCentipawnsForPlyNumber(ply_number);

        }
    }
}