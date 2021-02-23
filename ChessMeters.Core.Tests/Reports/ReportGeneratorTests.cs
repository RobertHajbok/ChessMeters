using ChessMeters.Core.Converters;
using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{

    public class ReportGeneratorTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public ReportGeneratorTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("ChessMeters");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Schedule_Should_ParsePGNAndScheduleReport()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());

            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess);
            var engineAnalyzeEvaluator = new EngineEvaluationBuilder(stockfishEngine, context);
            var gameAnalyzer = new TreeMovesBuilder(context, engineAnalyzeEvaluator);

            var gameConverter = new GameConverter();

            var reportGenerator = new ReportGenerator(gameAnalyzer, null, context);
            var reportId = await context.Reports.Select(x => x.Id).FirstAsync();

            var report = await reportGenerator.Schedule(reportId, EngineConsts.defaultAnalyzeDepth);

            Assert.NotNull(report);
        }
    }
}