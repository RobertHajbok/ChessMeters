using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public interface IRule
    {
        bool IsGameRule { get; }

        FlagEnum? Evaluate(IBoardState board);
    }
}