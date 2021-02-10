using ChessMeters.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public interface ITreeMovesBuilder
    {
        Task<IEnumerable<TreeMove>> BuildTree(short analyzeDepth, Game game);
    }
}