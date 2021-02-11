using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class CastlingRule : IRule
    {
        public bool IsGameRule { get { return true; } }

        public FlagEnum? Evaluate(IBoardState board)
        {
            // This rule applies only after move 10;
            if (board.CurrentTreeMove.MoveNumber < 10)
            {
                return null;
            }
            else if (board.UserColor != board.CurrentTreeMove.ColorId)
            {
                return null;
            }

            // This rule applies only if player did not castle yet.
            var didAlreadyCastle = board.CurrentTreeMove.ColorId == ColorEnum.White ?
                (board.WhiteCastledShort || board.WhiteCastledLong) :
                (board.BlackCastledShort || board.BlackCastledLong);
            if (didAlreadyCastle)
            {
                return null;
            }

            // This rule applies only if player is still not castling.
            if (board.CurrentTreeMove.ColorId == ColorEnum.White && board.IsWhiteCastling)
            {
                return null;
            }
            if (board.CurrentTreeMove.ColorId == ColorEnum.Black && board.IsBlackCastling)
            {
                return null;
            }

            return FlagEnum.DidNotCastle;
        }
    }
}