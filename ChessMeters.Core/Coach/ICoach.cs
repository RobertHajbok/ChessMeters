using ChessMeters.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessMeters.Core.Coach
{
    public interface ICoach
    {
        Task AnalizeGame(Game game, IEnumerable<ICoachRule> rules);
    }
}