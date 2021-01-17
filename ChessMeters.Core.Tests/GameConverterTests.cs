using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class GameConverterTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ConvertFromPGN_Should_GetGameFromPGN()
        {
            var gameConverter = new GameConverter();
            var game = await gameConverter.ConvertFromPGN("1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1");

            Assert.NotNull(game);
            Assert.Equal("0-1", game.Result);
            Assert.Equal("e2e4 c7c5 f2f4 d7d5 e4e5", game.Moves);
        }
    }
}