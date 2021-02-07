namespace ChessMeters.Core.Coach
{
    public class CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5 : ICoachFlag
    {
        private int playerColor;
        private int plyNumber;
        private int moveNumber;

        public CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5(ICoachBoard board)
        {
            playerColor = board.IsCurrentPlyWhite() ? 0 : 1;
            plyNumber = board.GetCurrentPlyNumber();
            moveNumber = board.GetCurrentMoveNumber();
        }

        public string GetDescription()
        {
            return "Did not develop at least 1 minor piece before move 5.";
        }

        public int GetPlayerColor()
        {
            return playerColor;
        }
    }
}