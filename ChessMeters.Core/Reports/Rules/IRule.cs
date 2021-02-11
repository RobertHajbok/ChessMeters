using ChessMeters.Core.Enums;

namespace ChessMeters.Core.Reports
{
    public interface IRule
    {
        bool IsGameRule { get; }

        FlagEnum Flag { get; }

        bool Evaluate(IBoardState board);
    }
}