namespace ChessMeters.Core.Coach
{
    public class CoachRuleDevelopAtLeastOneMinorPieceBeforeMove5 : ICoachRule
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
            if (board.CurrentTreeMove.MoveNumber < 5)
            {
                return null;
            }

            if (board.GetBlackDevelopedMinorPiecesCount() == 0)
            {
                isDisabled = true;
                return FlagEnum.DidNotDevelopAtLeastOneMinorPieceBeforeMove5;
            }

            return null;
        }
    }
}