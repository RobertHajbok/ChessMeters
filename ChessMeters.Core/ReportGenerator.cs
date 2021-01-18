using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class ReportGenerator: IReportGenerator
    {
        private readonly IGameConverter gameConverter;
        private readonly ITreeMovesBuilder treeMoveBuilder;

        public ReportGenerator(IGameConverter gameConverter, ITreeMovesBuilder treeMoveBuilder)
        {
            this.treeMoveBuilder = treeMoveBuilder;
            this.gameConverter = gameConverter;
        }

        public async Task Schedule(Report report, short engineDepth)
        {
            var games = await gameConverter.ConvertFromPGN(report.PGN);
            foreach (var game in games)
            {
                await treeMoveBuilder.BuildTree(engineDepth, game.Moves);
            }
        }
    }
}