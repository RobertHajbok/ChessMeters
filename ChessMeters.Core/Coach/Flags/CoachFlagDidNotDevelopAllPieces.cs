namespace ChessMeters.Core.Coach
{
    public class CoachFlagDidNotDevelopAllPieces : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;

        public CoachFlagDidNotDevelopAllPieces(ICoachBoard board)
        {
            playerColor = board.IsCurrentPlyWhite() ? 0 : 1;
            plyNumber = board.GetCurrentPlyNumber();
            moveNumber = board.GetCurrentMoveNumber();
        }

        public string GetDescription()
        {
            return "Did not develop all pieces in first n moves.";
        }

        public int GetPlayerColor()
        {
            return playerColor;
        }
    }
}