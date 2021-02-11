using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class CastlingRule : IRule
    {
        public bool IsGameRule { get { return true; } }

        public FlagEnum Flag { get { return FlagEnum.DidNotCastle; } }

        public bool Evaluate(IBoardState board)
        {
            // This rule applies only after move 10;
            if (board.CurrentTreeMove.MoveNumber < 10 || board.UserColor != board.CurrentTreeMove.ColorId)
            {
                return false;
            }

            // This rule applies only if player did not castle yet.
            var didAlreadyCastle = board.CurrentTreeMove.ColorId == ColorEnum.White ?
                (board.WhiteCastledShort || board.WhiteCastledLong) :
                (board.BlackCastledShort || board.BlackCastledLong);
            if (didAlreadyCastle || (board.CurrentTreeMove.ColorId == ColorEnum.White && board.IsWhiteCastling) ||
                (board.CurrentTreeMove.ColorId == ColorEnum.Black && board.IsBlackCastling))
            {
                return false;
            }

            return true;
        }
    }
}