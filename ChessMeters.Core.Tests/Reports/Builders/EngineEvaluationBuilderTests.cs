using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class EngineEvaluationBuilderTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public EngineEvaluationBuilderTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task BuildEngineEvaluations_Should_EvaluateWithStockfishAndSaveToDbIfEvaluationNotFound()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());
            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess);
            var engineAnalyzeEvaluator = new EngineEvaluationBuilder(stockfishEngine, context);

            var treeMove = await context.TreeMoves.FirstAsync(x => !x.ParentTreeMoveId.HasValue);
            await engineAnalyzeEvaluator.StartNewGame(10);
            var result = await engineAnalyzeEvaluator.BuildEngineEvaluations(treeMove);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }
    }
}
