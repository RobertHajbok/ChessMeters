using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class StockfishEngineTests
    {
        [Fact]
        public async Task StartAnalyse_Should_StartAnalyzingInitialPositionOnWindows()
        {
            var stockfishEngine = new StockfishEngine();
            var result = await stockfishEngine.StartAnalyse(20);

            Assert.Contains("info depth 20", result);
            Assert.Contains("bestmove", result);
            Assert.Contains("ponder", result);
        }
    }
}
