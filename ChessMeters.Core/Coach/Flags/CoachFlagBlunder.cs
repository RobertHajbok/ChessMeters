namespace ChessMeters.Core.Coach
{
    public class CoachFlagBlunder : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;

        public CoachFlagBlunder(ICoachBoard board)
        {
            this.playerColor = board.IsCurrentPlyWhite() ? 0 : 1;
            this.plyNumber = board.GetCurrentPlyNumber();
            this.moveNumber = board.GetCurrentMoveNumber();
        }
        public string GetDescription() 
        {
            return "Blundered a piece.";
        }

        public int GetPlayerColor()
        {
            return this.playerColor;
        }
    }
}