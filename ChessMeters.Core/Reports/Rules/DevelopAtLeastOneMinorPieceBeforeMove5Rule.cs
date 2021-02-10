using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public class DevelopAtLeastOneMinorPieceBeforeMove5Rule : IRule
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
            if (board.CurrentTreeMove.MoveNumber < 5)
            {
                return null;
            }

            if ((board.CurrentTreeMove.ColorId == ColorEnum.White && board.WhiteDevelopedMinorPiecesCount == 0) ||
                (board.CurrentTreeMove.ColorId == ColorEnum.Black && board.BlackDevelopedMinorPiecesCount == 0))
            {
                isDisabled = true;
                return FlagEnum.DidNotDevelopAtLeastOneMinorPieceBeforeMove5;
            }

            return null;
        }
    }
}