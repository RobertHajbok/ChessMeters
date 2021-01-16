using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IEngineAnalyzeEvaluator
    {
        Task StartNewGame(short engineDepth);

        Task<EngineEvaluation> BuildEngineEvaluations(TreeMove treeMove);
    }
}