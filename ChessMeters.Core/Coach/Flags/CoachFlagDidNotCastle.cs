namespace ChessMeters.Core.Coach
{
    public class CoachFlagDidNotCastle : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;

        public CoachFlagDidNotCastle(ICoachBoard board)
        {
            playerColor = board.IsCurrentPlyWhite() ? 0 : 1;
            plyNumber = board.GetCurrentPlyNumber();
            moveNumber = board.GetCurrentMoveNumber();
        }

        public string GetDescription()
        {
            return "Did not castle in first n moves.";
        }

        public int GetPlayerColor()
        {
            return playerColor;
        }
    }
}