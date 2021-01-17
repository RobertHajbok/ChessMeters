using ChessMeters.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IGameConverter
    {
        Task<IEnumerable<Game>> ConvertFromPGN(string pgn);
    }
}