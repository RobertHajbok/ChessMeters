using ChessMeters.Core.Engines;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class StockfishEngineTests
    {
        [Fact]
        public async Task AnalyzePosition_Should_ShouldAnalyzePositionAfterACoupleOfMoves()
        {
            var engineProcess = new EngineProcess();
            var stockfishEngine = new StockfishEngine(engineProcess, 10);
            await stockfishEngine.Initialize();
            await stockfishEngine.SetPosition("e2e4", "e7e5", "f1c4", "f8e7");
            var result = await stockfishEngine.AnalyzePosition();

            Assert.Contains("bestmove", result);
            Assert.Contains("d1h5", result);
            Assert.Contains("ponder", result);
        }
    }
}
