namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5White : ICoachRule
    {
        private bool isDisabled = false;

        public ICoachFlag Evaluate(ICoachBoard board)
        {
            if (isDisabled)
            {
                return null;
            }

            // This rule applies only after move 12;
            if (board.GetCurrentMoveNumber() < 5)
            {
                return null;
            }

            if (board.IsCurrentPlyWhite() && board.GetWhiteDevelopedMinorPiecesCount() == 0)
            {
                isDisabled = true;
                return new CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5(board);
            }

            return null;
        }
    }
}