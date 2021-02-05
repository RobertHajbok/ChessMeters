namespace ChessMeters.Core.Coach
{
    public interface ICoachRule
    {
        public ICoachFlag Evaluate(ICoachBoard board);
    }
}