using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class EngineAnalyzeEvaluator : IEngineAnalyzeEvaluator
    {
        private readonly IEngine engine;
        private readonly ChessMetersContext chessMetersContext;

        public EngineAnalyzeEvaluator(IEngine engine, ChessMetersContext chessMetersContext)
        {
            this.engine = engine;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task BuildEngineEvaluations(IEnumerable<TreeMove> treeMoves, short depth)
        {
            if (!treeMoves.Any())
                return;

            await engine.Initialize();

            for (var i = 0; i < treeMoves.Count(); i++)
            {
                var treeMove = treeMoves.ElementAt(i);
                var engineEvaluation = await chessMetersContext.EngineEvaluations.SingleOrDefaultAsync(x => x.TreeMoveId == treeMove.Id && x.EngineId == engine.EngineId);
                if (engineEvaluation?.Depth >= depth)
                    continue;

                //await engine.SetPosition(..); TODO: set position
                var evaluationCentipawns = await engine.GetEvaluationCentipawns();

                if (engineEvaluation != null)
                {
                    engineEvaluation.Depth = depth;
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
                        Depth = depth,
                        EvaluationCentipawns = evaluationCentipawns
                    });
                    await chessMetersContext.SaveChangesAsync();
                }
            }
        }
    }
}
