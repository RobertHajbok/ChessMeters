using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly ITreeMovesBuilder treeMoveBuilder;
        private readonly IFlagBuilder coach;
        private readonly ChessMetersContext chessMetersContext;

        public ReportGenerator(ITreeMovesBuilder treeMoveBuilder, IFlagBuilder coach, ChessMetersContext chessMetersContext)
        {
            this.treeMoveBuilder = treeMoveBuilder;
            this.coach = coach;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task<Report> Schedule(int reportId, short engineDepth)
        {
            var report = await chessMetersContext.Reports.Include(x => x.Games).SingleAsync(x => x.Id == reportId);

            foreach (var game in report.Games)
            {
                try
                {
                    var treeMoves = await treeMoveBuilder.BuildTree(engineDepth, game);
                    game.LastTreeMoveId = treeMoves.LastOrDefault()?.Id;
                    game.Analyzed = true;
                    await coach.AnalizeGame(game);
                    chessMetersContext.Games.Update(game);
                }
                catch (Exception ex)
                {
                    game.AnalyzeExceptionStackTrace = ex.ToString();
                }
                finally
                {
                    await chessMetersContext.SaveChangesAsync();
                }
            }

            return report;
        }
    }
}