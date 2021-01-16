using ChessMeters.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class TreeMovesBuilderTests
    {
        private readonly DbContextOptions<ChessMetersContext> options;

        public TreeMovesBuilderTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string mySqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ChessMetersContext>();
            options = optionsBuilder.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).UseLazyLoadingProxies().Options;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task BuildTree_Should_SetupTreeAndSaveToDbIfMoveNotFound()
        {
            using var context = new ChessMetersContext(options, new OperationalStoreOptionsMigrations());
            var gameAnalyzer = new TreeMovesBuilder(context);
            var moves = await gameAnalyzer.BuildTree("e2e4", "e7e5", "f1c4", "f8e7", "d1h5");

            Assert.Equal(5, moves.Count());
            Assert.All(moves, x => Assert.NotEqual(0, x.Id));
            Assert.Null(moves.First().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMoveId);
            Assert.NotNull(moves.Last().ParentTreeMove);
        }
    }
}
