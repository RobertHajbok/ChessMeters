using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using ChessMeters.Core.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Coach.Tests
{
    public class CoachRuleDevelopPiecesTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public CoachRuleDevelopPiecesTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_1()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white develops normal
            // black doesn't develop any minor piece
            var algebraic = "e2e4 d7d5 e4d5 f7f6 d2d3 e7e5 g1f3 g7g6 b1c3 a7a6 f1e2 b7b6 e1g1 h7h6 c1e3 g6g5 d1d2 b6b5 a1b1 a6a5 f1e1 a5a4 g2g3 h6h5 e2f1 c7c5 f1g2 a4a3 b2b3 b5b4 c3b5 c5c4";
            var game = await CreateGame(context, algebraic);

            //var board = new CoachBoard(game);
            //var rules = new List<ICoachRule>()
            //{
            //    new CoachRuleDevelopMinorPieces()
            //};

            //var coach = new Coach(board, rules);
            //var flags = coach.AnalizeGame();

            //AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            //AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
            Assert.True(0 == 1);
        }

        [Fact]
        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_2()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white doesn't develop any minor piece
            // black develops normaal
            var algebraic = "a2a3 g8f6 b2b3 b8c6 c2c3 e7e6 d2d3 d7d6 e2e3 f8e7 f2f3 c8d7 g2g3 e8g8 h2h4 d8c8 a3a4 f8e8 b3b4 e8f8 c3c4 c8b8 d3d4 f8e8 e3e4 e8f8 f3f4 b8c8 g3g4 f8d8 h4h5";
            var game = await CreateGame(context, algebraic);

            //var board = new CoachBoard(game);
            //var rules = new List<ICoachRule>()
            //{
            //    new CoachRuleDevelopMinorPieces()
            //};

            //var coach = new Coach(board, rules);
            //var flags = coach.AnalizeGame();

            //AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            //AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
            Assert.True(0 == 1);
        }

        [Fact]
        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_3()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white develops normal
            // black doesn't develop one knight
            var algebraic = "e2e4 c7c5 g1f3 e7e6 d2d4 c5d4 f3d4 a7a6 b1c3 d8c7 f1d3 b8c6 e1g1 b7b5 c1e3 c8b7 f2f4 h7h6 g1h1 f8b4 a2a3 b4c5 f1f3 c5d4 e3d4 c6d4 d3b5 a6b5 d1d4";
            var game = await CreateGame(context, algebraic);

            //var board = new CoachBoard(game);
            //var rules = new List<ICoachRule>()
            //{
            //    new CoachRuleDevelopMinorPieces()
            //};

            //var coach = new Coach(board, rules);
            //var flags = coach.AnalizeGame();

            //AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            //AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
            Assert.True(0 == 1);
        }

        // TODO: remove duplication.
        private async Task<Game> CreateGame(ChessMetersContext context, string algebraic_moves)
        {
            // TODO: this method should receive a pgn as param.
            string pgn = "[Event \"Live Chess\"] [Site \"Chess.com\"] [Date \"2020.11.25\"] [Round \"?\"] [White \"claudiuoprea\"] [Black \"Cest_Sebastian\"] [Result \"0-1\"] [ECO \"C26\"] [WhiteElo \"1174\"] [BlackElo \"1212\"] [TimeControl \"600\"] [EndTime \"5:58:12 PST\"] [Termination \"Cest_Sebastian won by resignation\"] 1. e4 {[%timestamp 1]} 1... e5 {[%timestamp 1]} 2. Bc4 {[%timestamp 1]} 2... Nf6 {[%timestamp 1]} 3. Nc3 {[%timestamp 16]} 3... Nc6 {[%timestamp 253]} 4. d3 {[%timestamp 21]} 4... Bb4 {[%timestamp 93]} 5. f4 {[%timestamp 79]} 5... Bxc3+ {[%timestamp 70]} 6. bxc3 {[%timestamp 10]} 6... O-O {[%timestamp 80]} 7. fxe5 {[%timestamp 91]} 7... Nxe5 {[%timestamp 66]} 8. Bb3 {[%timestamp 33]} 8... d6 {[%timestamp 52]} 9. Nf3 {[%timestamp 24]} 9... Bg4 {[%timestamp 27]} 10. h3 {[%timestamp 114]} 10... Bxf3 {[%timestamp 14]} 11. gxf3 {[%timestamp 39]} 11... d5 {[%timestamp 33]} 12. Rg1 {[%timestamp 107]} 12... dxe4 {[%timestamp 58]} 13. Bh6 {[%timestamp 48]} 13... Re8 {[%timestamp 135]} 14. Bxg7 {[%timestamp 126]} 14... Ng6 {[%timestamp 1]} 15. Bh6 {[%timestamp 250]} 15... exf3+ {[%timestamp 59]} 16. Kd2 {[%timestamp 49]} 16... f2 {[%timestamp 174]} 17. Rg2 {[%timestamp 187]} 17... Ne4+ {[%timestamp 237]} 18. Kc1 {[%timestamp 74]} 18... Nxc3 {[%timestamp 142]} 19. Qd2 {[%timestamp 109]} 19... f1=Q+ {[%timestamp 58]} 20. Kb2 {[%timestamp 60]} 20... Na4+ {[%timestamp 431]} 21. Bxa4 {[%timestamp 175]} 21... Qd4+ {[%timestamp 76]} 22. Ka3 {[%timestamp 128]} 22... Qc5+ {[%timestamp 114]} 23. Kb3 {[%timestamp 112]} 23... Qd5+ {[%timestamp 60]} 24. Ka3 {[%timestamp 35]} 24... Qfxg2 {[%timestamp 40]} 25. Qc3 {[%timestamp 74]} 25... Qd6+ {[%timestamp 179]} 26. Kb3 {[%timestamp 149]} 26... Qgd5+ {[%timestamp 69]} 27. Kb2 {[%timestamp 29]} 27... Qb6+ {[%timestamp 96]} 28. Bb3 {[%timestamp 52]} 28... Qdd4 {[%timestamp 1]} 29. Qxd4 {[%timestamp 1]} 29... Qxd4+ {[%timestamp 1]} 30. Ka3 {[%timestamp 31]} 0-1";

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
                Moves = algebraic_moves
            });


            var games = report.Games;
            return games.First();
        }

        // Assert (TODO: remove duplication)
        private void AssertFlagsContain(IEnumerable<FlagEnum> flags, FlagEnum expectedFlag)
        {
            Assert.True(
                FlagExistsForColor(flags, expectedFlag)
            );
        }

        private void AssertFlagsNotContain(IEnumerable<FlagEnum> flags, FlagEnum expectedFlag)
        {
            Assert.True(
                !FlagExistsForColor(flags, expectedFlag)
            );
        }

        private bool FlagExistsForColor(IEnumerable<FlagEnum> flags, FlagEnum expectedFlag)
        {
            return flags.Any(flag => flag == expectedFlag);
        }
    }
}