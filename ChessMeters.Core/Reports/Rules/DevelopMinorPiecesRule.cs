using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class DevelopMinorPiecesRule : IRule
    {
        private bool isDisabled = false;

        public bool IsGameRule { get { return true; } }

        public FlagEnum Flag { get { return FlagEnum.DidNotDevelopAllPieces; } }

        public bool Evaluate(IBoardState board)
        {
            if (isDisabled || board.UserColor != board.CurrentTreeMove.ColorId ||
                board.CurrentTreeMove.MoveNumber < 12)
            {
                return false;
            }

            if ((board.CurrentTreeMove.ColorId == ColorEnum.White && !board.DidWhiteAlreadyDevelopAllMinorPieces) ||
                (board.CurrentTreeMove.ColorId == ColorEnum.Black && !board.DidBlackAlreadyDevelopAllMinorPieces))
            {
                isDisabled = true;
                return true;
            }

            return false;
        }
    }
}