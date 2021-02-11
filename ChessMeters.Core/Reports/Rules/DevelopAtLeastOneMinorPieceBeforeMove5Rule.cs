using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class DevelopAtLeastOneMinorPieceBeforeMove5Rule : IRule
    {
        private bool isDisabled = false;

        public bool IsGameRule { get { return true; } }

        public FlagEnum Flag { get { return FlagEnum.DidNotDevelopAtLeastOneMinorPieceBeforeMove5; } }

        public bool Evaluate(IBoardState board)
        {
            if (isDisabled || board.UserColor != board.CurrentTreeMove.ColorId ||
                board.CurrentTreeMove.MoveNumber < 5)
            {
                return false;
            }

            if ((board.CurrentTreeMove.ColorId == ColorEnum.White && board.WhiteDevelopedMinorPiecesCount == 0) ||
                (board.CurrentTreeMove.ColorId == ColorEnum.Black && board.BlackDevelopedMinorPiecesCount == 0))
            {
                isDisabled = true;
                return true;
            }

            return false;
        }
    }
}