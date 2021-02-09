namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopMinorPieces : ICoachRule
    {
        private bool isDisabled = false;

        public bool IsGameRule { get { return true; } }

        public FlagEnum? Evaluate(ICoachBoard board)
        {
            if (isDisabled)
            {
                return null;
            }

            // This rule applies only after move 12;
            if (board.CurrentTreeMove.MoveNumber < 12)
            {
                return null;
            }

            if (!board.DidWhiteAlreadyDevelopAllMinorPieces())
            {
                isDisabled = true;
                return FlagEnum.DidNotDevelopAllPieces;
            }

            return null;
        }
    }
}