using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class TreeMovesBuilderTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public TreeMovesBuilderTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task BuildTree_Should_SetupTreeAndSaveToDbIfMoveNotFound()
        {
            string pgn = "[Event \"Live Chess\"] [Site \"Chess.com\"] [Date \"2020.11.25\"] [Round \"?\"] [White \"claudiuoprea\"] [Black \"Cest_Sebastian\"] [Result \"0-1\"] [ECO \"C26\"] [WhiteElo \"1174\"] [BlackElo \"1212\"] [TimeControl \"600\"] [EndTime \"5:58:12 PST\"] [Termination \"Cest_Sebastian won by resignation\"] 1. e4 {[%timestamp 1]} 1... e5 {[%timestamp 1]} 2. Bc4 {[%timestamp 1]} 2... Nf6 {[%timestamp 1]} 3. Nc3 {[%timestamp 16]} 3... Nc6 {[%timestamp 253]} 4. d3 {[%timestamp 21]} 4... Bb4 {[%timestamp 93]} 5. f4 {[%timestamp 79]} 5... Bxc3+ {[%timestamp 70]} 6. bxc3 {[%timestamp 10]} 6... O-O {[%timestamp 80]} 7. fxe5 {[%timestamp 91]} 7... Nxe5 {[%timestamp 66]} 8. Bb3 {[%timestamp 33]} 8... d6 {[%timestamp 52]} 9. Nf3 {[%timestamp 24]} 9... Bg4 {[%timestamp 27]} 10. h3 {[%timestamp 114]} 10... Bxf3 {[%timestamp 14]} 11. gxf3 {[%timestamp 39]} 11... d5 {[%timestamp 33]} 12. Rg1 {[%timestamp 107]} 12... dxe4 {[%timestamp 58]} 13. Bh6 {[%timestamp 48]} 13... Re8 {[%timestamp 135]} 14. Bxg7 {[%timestamp 126]} 14... Ng6 {[%timestamp 1]} 15. Bh6 {[%timestamp 250]} 15... exf3+ {[%timestamp 59]} 16. Kd2 {[%timestamp 49]} 16... f2 {[%timestamp 174]} 17. Rg2 {[%timestamp 187]} 17... Ne4+ {[%timestamp 237]} 18. Kc1 {[%timestamp 74]} 18... Nxc3 {[%timestamp 142]} 19. Qd2 {[%timestamp 109]} 19... f1=Q+ {[%timestamp 58]} 20. Kb2 {[%timestamp 60]} 20... Na4+ {[%timestamp 431]} 21. Bxa4 {[%timestamp 175]} 21... Qd4+ {[%timestamp 76]} 22. Ka3 {[%timestamp 128]} 22... Qc5+ {[%timestamp 114]} 23. Kb3 {[%timestamp 112]} 23... Qd5+ {[%timestamp 60]} 24. Ka3 {[%timestamp 35]} 24... Qfxg2 {[%timestamp 40]} 25. Qc3 {[%timestamp 74]} 25... Qd6+ {[%timestamp 179]} 26. Kb3 {[%timestamp 149]} 26... Qgd5+ {[%timestamp 69]} 27. Kb2 {[%timestamp 29]} 27... Qb6+ {[%timestamp 96]} 28. Bb3 {[%timestamp 52]} 28... Qdd4 {[%timestamp 1]} 29. Qxd4 {[%timestamp 1]} 29... Qxd4+ {[%timestamp 1]} 30. Ka3 {[%timestamp 31]} 0-1";

            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());
            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess);
            var engineAnalyzeEvaluator = new EngineEvaluationBuilder(stockfishEngine, context);
            var gameAnalyzer = new TreeMovesBuilder(context, engineAnalyzeEvaluator);

            var user = await context.Users.FirstAsync();
            var report = new Report
            {
                PGN = pgn,
                Description = $"Unit test ${DateTime.Now}",
                UserId = user.Id
            };

            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();

            var moves = await gameAnalyzer.BuildTree(10, new Game
            {
                ReportId = report.Id,
                Result = "0-1",
                Moves = "d2d4 d7d5 c2c4 c7c6 g1f3"
            });

            Assert.Equal(5, moves.Count());
            Assert.All(moves, x => Assert.NotEqual(0, x.Id));
            Assert.Null(moves.First().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMove);
        }
    }
}
