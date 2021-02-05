namespace ChessMeters.Core.Coach
{
    public class CoachFlagDidNotCastle : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;
        public CoachFlagDidNotCastle(ICoachBoard board)
        {
            this.playerColor = board.isCurrentPlyWhite() ? 0 : 1;
            this.plyNumber = board.GetCurrentPlyNumber();
            this.moveNumber = board.GetCurrentMoveNumber();
        }
        public string GetDescription() 
        {
            return "Did not castle in first n moves.";
        }

        public int GetPlayerColor()
        {
            return this.playerColor;
        }
    }
}