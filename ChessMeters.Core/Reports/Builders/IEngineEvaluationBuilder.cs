using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public interface IEngineEvaluationBuilder
    {
        Task StartNewGame(short engineDepth);

        Task<EngineEvaluation> BuildEngineEvaluations(TreeMove treeMove, params string[] previousMoves);
    }
}