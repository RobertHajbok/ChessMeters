using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class DevelopMinorPiecesRule : IRule
    {
        private bool isDisabled = false;

        public bool IsGameRule { get { return true; } }

        public FlagEnum? Evaluate(IBoardState board)
        {
            if (isDisabled)
            {
                return null;
            }

            // This rule applies only after move 12;
            if (board.CurrentTreeMove.MoveNumber < 12)
            {
                return null;
            }

            if ((board.CurrentTreeMove.ColorId == ColorEnum.White && !board.DidWhiteAlreadyDevelopAllMinorPieces) ||
                (board.CurrentTreeMove.ColorId == ColorEnum.Black && !board.DidBlackAlreadyDevelopAllMinorPieces))
            {
                isDisabled = true;
                return FlagEnum.DidNotDevelopAllPieces;
            }

            return null;
        }
    }
}