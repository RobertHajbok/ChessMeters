namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopMinorPiecesBlack : ICoachRule
    {
        private bool isDisabled = false;
        
        public ICoachFlag? Evaluate(ICoachBoard board)
        {
            if (this.isDisabled)
            {
                return null;
            }
            
            // This rule applies only after move 12;
            if (board.GetCurrentMoveNumber() < 12)
            {
                return null;
            }

            if (board.isCurrentPlyWhite() && !board.DidWhiteAlreadyDevelopAllMinorPieces())
            {
                this.isDisabled = true;
                return new CoachFlagDidNotDevelopAllPieces(board);
            }
            
            return null;
        }
    }
}