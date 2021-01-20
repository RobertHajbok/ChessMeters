using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly IGameConverter gameConverter;
        private readonly ITreeMovesBuilder treeMoveBuilder;
        private readonly ChessMetersContext chessMetersContext;

        public ReportGenerator(IGameConverter gameConverter, ITreeMovesBuilder treeMoveBuilder,
            ChessMetersContext chessMetersContext)
        {
            this.treeMoveBuilder = treeMoveBuilder;
            this.chessMetersContext = chessMetersContext;
            this.gameConverter = gameConverter;
        }

        public async Task<int> Schedule(Report report, short engineDepth)
        {
            var games = await gameConverter.ConvertFromPGN(report.PGN);
            await chessMetersContext.Reports.AddAsync(report);
            await chessMetersContext.SaveChangesAsync();

            foreach (var game in games)
            {
                game.ReportId = report.Id;
                await treeMoveBuilder.BuildTree(engineDepth, game);
            }

            return report.Id;
        }
    }
}