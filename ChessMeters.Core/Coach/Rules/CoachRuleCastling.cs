namespace ChessMeters.Core.Coach
{
    public class CoachRuleCastling : ICoachRule
    {
        public ICoachFlag Evaluate(ICoachBoard board)
        {
            // This rule applies only after move 10;
            if (board.GetCurrentMoveNumber() < 10)
            {
                return null;
            }

            // This rule applies only if player did not castle yet.
            var didAlreadyCastle = board.IsCurrentPlyWhite()
                ? board.DidWhiteAlreadyCastle()
                : board.DidBlackAlreadyCastle();
            if (didAlreadyCastle)
            {
                return null;
            }

            // This rule applies only if player is still not castling.
            if (board.IsCurrentPlyWhite() && board.IsWhiteCastling())
            {
                return null;
            }
            if (board.IsCurrentPlyBlack() && board.IsBlackCastling())
            {
                return null;
            }

            // Player did not castle, this is a flag.
            return new CoachFlagDidNotCastle(board);
        }
    }
}