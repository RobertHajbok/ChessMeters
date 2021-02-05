namespace ChessMeters.Core.Coach
{
    public class CoachFlagDidNotDevelopAllPieces : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;
        public CoachFlagDidNotDevelopAllPieces(ICoachBoard board)
        {
            this.playerColor = board.isCurrentPlyWhite() ? 0 : 1;
            this.plyNumber = board.GetCurrentPlyNumber();
            this.moveNumber = board.GetCurrentMoveNumber();
        }
        public string GetDescription() 
        {
            return "Did not develop all pieces in first n moves.";
        }

        public int GetPlayerColor()
        {
            return this.playerColor;
        }
    }
}