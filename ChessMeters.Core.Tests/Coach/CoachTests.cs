using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

using ChessMeters.Core.Coach;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ChessMeters.Core.Database;
using System.IO;
using ChessMeters.Core.Entities;
using ChessMeters.Core.Engines;
using System.Linq;
using System;

namespace ChessMeters.Core.Coach.Tests
{
    public class CoachTests
    {

        private readonly DbContextOptions<ChessMetersContext> options;
        public CoachTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_1()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white castled on move 8
            // black castled on move 11
            var algebraic = "e2e4 c7c5 g1f3 e7e6 b1c3 a7a6 d2d4 c5d4 f3d4 d8c7 f1e2 f8b4 c1d2 g8f6 e1g1 b4c3 d2c3 f6e4 d1d3 e4c3 b2c3 e8g8 e2f3 b8c6 a1b1 d7d5 f1e1 c6e5 d3e2 e5f3 e2f3 b7b5 e1e3 c8b7 f3g4 f7f5 g4g5 h7h6 g5g6 f8f6 g6h5 e6e5 d4f5 a8f8 f5h4 f6f2 e3e5 c7c3 e5f5 c3c2 f5f8 f2f8 b1f1 f8f1 g1f1 d5d4 h5e8 g8h7 e8d7 d4d3 d7f5 h7g8 f5e6 g8h7 e6f5 h7g8 f5e6 g8f8 h4g6";
            var game = await CreateGame(context, algebraic);

            var stockfishCentipawns = new CoachBoardStockfish(context, game);
            var board = new CoachBoard(game, stockfishCentipawns);
            var rules = new List<ICoachRule>()
            {
                new CoachRuleCastling(),
            };

            var coach = new Coach(board, rules);
            var flags = coach.AnalizeGame();

            // White castled on move 8, before move 10, should not flag anything.
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black castled on move 11, after move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_2()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white castled on move 10
            // black castled on move 17                                                                                 x                                                                          x
            var algebraic = "e2e4 c7c5 g1f3 e7e6 f1b5 a7a6 b5c4 b7b5 c4e2 d8c7 e4e5 b8c6 b1c3 c6e5 d2d4 e5f3 e2f3 d7d5 e1g1 c5d4 c3e2 f8d6 g2g3 f7f6 e2d4 e6e5 d4e2 e5e4 f3g2 g8e7 a2a4 b5b4 a4a5 e8g8 c1e3 d6c5 e3f4 c5d6 f4d6 c7d6 e2f4 e7c6 f4d5 f6f5 d5e3 a8b8 d1d6	";
            var game = await CreateGame(context, algebraic);

            var stockfishCentipawns = new CoachBoardStockfish(context, game);
            var board = new CoachBoard(game, stockfishCentipawns);
            var rules = new List<ICoachRule>()
            {
                new CoachRuleCastling(),
            };

            var coach = new Coach(board, rules);
            var flags = coach.AnalizeGame();

            // White castled on move 10, before move 10, should not flag anything.
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black castled on move 17, after move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_3()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            // white did not castle
            // black did not castle                                                                                                                                                      
            var algebraic = "e2e4 e7e5 f1c4 g8f6 d2d3 h7h6 f2f4 d7d6 g1f3 c8g4 h2h3 g4h5 g2g4 h5g6 g4g5 f6d7 f4f5 g6h7 g5g6 f7g6 f5g6 h7g6 h1f1 g6h5 d1d2 b8c6 b1c3 d8f6 c3d5 f6d8 b2b4 h5f3 f1f3 c6d4 f3f2 c7c6 d5e3 d8h4 c4f7 e8d8 d2d1 h4h3 e3f5 h3h1 e1d2 h1d1 d2d1 d7f6 c1b2 f6g4 f2d2 d4f5 e4f5 g4e3 d1e2 e3f5 e2f3 f8e7 f7e6 f5d4 b2d4 e5d4 f3e4 e7g5 d2f2 d8e7 f2f7 e7e6 a1f1 d6d5";

            var game = await CreateGame(context, algebraic);

            var stockfishCentipawns = new CoachBoardStockfish(context, game);
            var board = new CoachBoard(game, stockfishCentipawns);
            var rules = new List<ICoachRule>()
            {
                new CoachRuleCastling(),
            };

            var coach = new Coach(board, rules);
            var flags = coach.AnalizeGame();

            // White did not castle before move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black did not castle before move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        // TODO: remove duplication.
        private async Task<Game> CreateGame(ChessMetersContext context, string algebraic_moves)
        {
            // TODO: this method should receive a pgn as param.
            string pgn = "[Event \"Live Chess\"] [Site \"Chess.com\"] [Date \"2020.11.25\"] [Round \"?\"] [White \"claudiuoprea\"] [Black \"Cest_Sebastian\"] [Result \"0-1\"] [ECO \"C26\"] [WhiteElo \"1174\"] [BlackElo \"1212\"] [TimeControl \"600\"] [EndTime \"5:58:12 PST\"] [Termination \"Cest_Sebastian won by resignation\"] 1. e4 {[%timestamp 1]} 1... e5 {[%timestamp 1]} 2. Bc4 {[%timestamp 1]} 2... Nf6 {[%timestamp 1]} 3. Nc3 {[%timestamp 16]} 3... Nc6 {[%timestamp 253]} 4. d3 {[%timestamp 21]} 4... Bb4 {[%timestamp 93]} 5. f4 {[%timestamp 79]} 5... Bxc3+ {[%timestamp 70]} 6. bxc3 {[%timestamp 10]} 6... O-O {[%timestamp 80]} 7. fxe5 {[%timestamp 91]} 7... Nxe5 {[%timestamp 66]} 8. Bb3 {[%timestamp 33]} 8... d6 {[%timestamp 52]} 9. Nf3 {[%timestamp 24]} 9... Bg4 {[%timestamp 27]} 10. h3 {[%timestamp 114]} 10... Bxf3 {[%timestamp 14]} 11. gxf3 {[%timestamp 39]} 11... d5 {[%timestamp 33]} 12. Rg1 {[%timestamp 107]} 12... dxe4 {[%timestamp 58]} 13. Bh6 {[%timestamp 48]} 13... Re8 {[%timestamp 135]} 14. Bxg7 {[%timestamp 126]} 14... Ng6 {[%timestamp 1]} 15. Bh6 {[%timestamp 250]} 15... exf3+ {[%timestamp 59]} 16. Kd2 {[%timestamp 49]} 16... f2 {[%timestamp 174]} 17. Rg2 {[%timestamp 187]} 17... Ne4+ {[%timestamp 237]} 18. Kc1 {[%timestamp 74]} 18... Nxc3 {[%timestamp 142]} 19. Qd2 {[%timestamp 109]} 19... f1=Q+ {[%timestamp 58]} 20. Kb2 {[%timestamp 60]} 20... Na4+ {[%timestamp 431]} 21. Bxa4 {[%timestamp 175]} 21... Qd4+ {[%timestamp 76]} 22. Ka3 {[%timestamp 128]} 22... Qc5+ {[%timestamp 114]} 23. Kb3 {[%timestamp 112]} 23... Qd5+ {[%timestamp 60]} 24. Ka3 {[%timestamp 35]} 24... Qfxg2 {[%timestamp 40]} 25. Qc3 {[%timestamp 74]} 25... Qd6+ {[%timestamp 179]} 26. Kb3 {[%timestamp 149]} 26... Qgd5+ {[%timestamp 69]} 27. Kb2 {[%timestamp 29]} 27... Qb6+ {[%timestamp 96]} 28. Bb3 {[%timestamp 52]} 28... Qdd4 {[%timestamp 1]} 29. Qxd4 {[%timestamp 1]} 29... Qxd4+ {[%timestamp 1]} 30. Ka3 {[%timestamp 31]} 0-1";

            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess);
            var engineAnalyzeEvaluator = new EngineAnalyzeEvaluator(stockfishEngine, context);
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

        private void AssertFlagsContain(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            Assert.True(
                FlagExistsForColor(flags, expected_flag_type, player_color)
            );
        }

        private void AssertFlagsNotContain(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            Assert.True(
                !FlagExistsForColor(flags, expected_flag_type, player_color)
            );
        }

        private bool FlagExistsForColor(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            return flags.Exists(
                flag => (flag.GetType() == expected_flag_type) && (flag.GetPlayerColor() == player_color)
            );
        }
    }
}
