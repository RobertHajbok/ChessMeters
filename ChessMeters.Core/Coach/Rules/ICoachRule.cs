namespace ChessMeters.Core.Coach
{
    public interface ICoachRule
    {
        bool IsGameRule { get; }

        FlagEnum? Evaluate(ICoachBoard board);
    }
}