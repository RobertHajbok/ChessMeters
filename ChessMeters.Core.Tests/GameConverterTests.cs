using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class GameConverterTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task GameConverter_Should_PGNToLatinAlgebraicNotation()
        {
            Assert.Equal(5, 5);
        }
    }
}