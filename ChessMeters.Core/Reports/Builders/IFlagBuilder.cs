using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public interface IFlagBuilder
    {
        Task AnalizeGame(Game game);
    }
}