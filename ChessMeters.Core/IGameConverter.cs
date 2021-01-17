using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IGameConverter
    {
        Task<Game> ConvertFromPGN(string pgn);
    }
}