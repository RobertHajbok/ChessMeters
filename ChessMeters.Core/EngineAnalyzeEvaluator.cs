using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class EngineAnalyzeEvaluator : IEngineAnalyzeEvaluator
    {
        private readonly IEngine engine;
        private readonly ChessMetersContext chessMetersContext;
        private short engineDepth;

        public EngineAnalyzeEvaluator(IEngine engine, ChessMetersContext chessMetersContext)
        {
            this.engine = engine;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task StartNewGame(short engineDepth)
        {
            this.engineDepth = engineDepth;
            await engine.Initialize(engineDepth);
        }

        public async Task<EngineEvaluation> BuildEngineEvaluations(TreeMove treeMove, params string[] previousMoves)
        {
            var engineEvaluation = await chessMetersContext.EngineEvaluations.SingleOrDefaultAsync(x => x.TreeMoveId == treeMove.Id && x.EngineId == engine.EngineId);
            if (engineEvaluation?.Depth >= engineDepth)
                return engineEvaluation;

            await engine.SetPosition(previousMoves.Any() ? $"{string.Join(' ', previousMoves)} {treeMove.Move}" : treeMove.Move);
            var evaluationCentipawns = await engine.GetEvaluationCentipawns(treeMove.Color);

            if (engineEvaluation != null)
            {
                engineEvaluation.Depth = engineDepth;
                engineEvaluation.EvaluationCentipawns = evaluationCentipawns;
                chessMetersContext.Update(engineEvaluation);
                await chessMetersContext.SaveChangesAsync();
            }
            else
            {
                await chessMetersContext.EngineEvaluations.AddAsync(new EngineEvaluation
                {
                    TreeMoveId = treeMove.Id,
                    EngineId = engine.EngineId,
                    Depth = engineDepth,
                    EvaluationCentipawns = evaluationCentipawns
                });
                await chessMetersContext.SaveChangesAsync();
            }
            return engineEvaluation;
        }
    }
}
