using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class GameAnalyzerTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public GameAnalyzerTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task AnalyzeGame_Should_ShouldAnalyzeGameAndSaveToDb()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());
            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess, 10);
            var gameAnalyzer = new GameAnalyzer(stockfishEngine, context);
            var moves = await gameAnalyzer.AnalizeGame("e2e4", "e7e5", "f1c4", "f8e7", "d1h5");

            Assert.Equal(5, moves.Count());
            Assert.All(moves, x => Assert.NotEqual(0, x.Id));
            Assert.Null(moves.First().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMove);
        }
    }
}
