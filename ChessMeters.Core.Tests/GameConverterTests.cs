using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Tests
{
    public class GameConverterTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ConvertFromPGN_Should_GetOneGameFromPGN()
        {
            string pgn = "1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1";
            int expected_games_count = 1;

            var gameConverter = new GameConverter();
            var games = await gameConverter.ConvertFromPGN(pgn);

            Assert.Equal(games.Count(), expected_games_count);
            AssertGame(games.First(), "0-1", "e2e4 c7c5 f2f4 d7d5 e4e5");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ConvertFromPGN_Should_GetMultipleGamesFromPGN()
        {
            string pgn = "1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1 1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1";
            int expected_games_count = 2;

            var gameConverter = new GameConverter();
            var games = await gameConverter.ConvertFromPGN(pgn);

            Assert.Equal(games.Count(), expected_games_count);
            AssertGame(games.First(), "0-1", "e2e4 c7c5 f2f4 d7d5 e4e5");
            AssertGame(games.ElementAt(1), "0-1", "e2e4 c7c5 f2f4 d7d5 e4e5");
        }

        private void AssertGame(Entities.Game game, string expected_result, string expected_algebraic_notation)
        {
            Assert.Equal(game.Result, expected_result);
            Assert.Equal(game.Moves, expected_algebraic_notation);
        }
    }
}