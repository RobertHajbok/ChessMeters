namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopMinorPiecesWhite : ICoachRule
    {
        private bool isDisabled = false;

        public ICoachFlag Evaluate(ICoachBoard board)
        {
            if (isDisabled)
            {
                return null;
            }

            // This rule applies only after move 12;
            if (board.GetCurrentMoveNumber() < 12)
            {
                return null;
            }

            if (board.IsCurrentPlyBlack() && !board.DidBlackAlreadyDevelopAllMinorPieces())
            {
                isDisabled = true;
                return new CoachFlagDidNotDevelopAllPieces(board);
            }

            return null;
        }
    }
}