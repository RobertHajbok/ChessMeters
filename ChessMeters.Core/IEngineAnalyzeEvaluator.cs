using ChessMeters.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IEngineAnalyzeEvaluator
    {
        Task BuildEngineEvaluations(IEnumerable<TreeMove> treeMoves, short depth);
    }
}