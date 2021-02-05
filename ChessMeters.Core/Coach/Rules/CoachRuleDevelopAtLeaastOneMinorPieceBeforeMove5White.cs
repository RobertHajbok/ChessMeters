namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5White : ICoachRule
    {
        private bool isDisabled = false;
        
        public ICoachFlag? Evaluate(ICoachBoard board)
        {
            if (this.isDisabled)
            {
                return null;
            }
            
            // This rule applies only after move 12;
            if (board.GetCurrentMoveNumber() < 5)
            {
                return null;
            }

            if (board.isCurrentPlyWhite() && board.GetWhiteDevelopedMinorPiecesCount() == 0)
            {
                this.isDisabled = true;
                return new CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5(board);
            }
            
            return null;
        }
    }
}