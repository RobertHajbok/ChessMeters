using System.Collections.Generic;

namespace ChessMeters.Core.Coach
{
    public interface ICoach
    {
        public List<ICoachFlag> AnalizeGame();
    }
}