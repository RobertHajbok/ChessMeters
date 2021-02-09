namespace ChessMeters.Core.Coach
{
    public class CoachRuleCastling : ICoachRule
    {
        public bool IsGameRule { get { return true; } }

        public FlagEnum? Evaluate(ICoachBoard board)
        {
            // This rule applies only after move 10;
            if (board.CurrentTreeMove.MoveNumber < 10)
            {
                return null;
            }

            // This rule applies only if player did not castle yet.
            var didAlreadyCastle = board.CurrentTreeMove.ColorId == ColorEnum.White
                ? board.DidWhiteAlreadyCastle()
                : board.DidBlackAlreadyCastle();
            if (didAlreadyCastle)
            {
                return null;
            }

            // This rule applies only if player is still not castling.
            if (board.CurrentTreeMove.ColorId == ColorEnum.White && board.IsWhiteCastling())
            {
                return null;
            }
            if (board.CurrentTreeMove.ColorId == ColorEnum.Black && board.IsBlackCastling())
            {
                return null;
            }

            return FlagEnum.DidNotCastle;
        }
    }
}