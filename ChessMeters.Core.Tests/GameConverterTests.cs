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

        [Fact]
        [Trait("Category", "Unit")]
        public async Task ConvertFromPGN_Should_SetGamePropertie()
        {
            string pgn = @"[Event ""My Event""] [Site ""My Chessmeters.com""] [Round ""My Final""] [White ""User 1""] [Black ""User 2""] [WhiteElo ""1138""] [BlackElo ""1196""] [ECO ""B23""] [TimeControl ""1800""] [Termination ""claudiuoprea won by checkmate""] 1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1";

            var gameConverter = new GameConverter();
            var games = await gameConverter.ConvertFromPGN(pgn);
            var game = games.First();

            Assert.Equal("My Event", game.Event);
            Assert.Equal("My Chessmeters.com", game.Site);
            Assert.Equal("My Final", game.Round);
            Assert.Equal("User 1", game.White);
            Assert.Equal("User 2", game.Black);
            Assert.Equal(1138, game.WhiteElo);
            Assert.Equal(1196, game.BlackElo);
            Assert.Equal("B23", game.Eco);
            Assert.Equal("1800", game.TimeControl);
            Assert.Equal("claudiuoprea won by checkmate", game.Termination);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task ConvertFromPGN_Should_NotSetGameEmptyPropertie()
        {
            string pgn = @"[Event ""?""] [Site ""?""] [Round ""?""] [White ""?""] [Black ""?""] 1. e4 c5 2. f4 { B21 Sicilian Defense: McDonnell Attack } d5 3. e5 { Black resigns. } 0-1";

            var gameConverter = new GameConverter();
            var games = await gameConverter.ConvertFromPGN(pgn);
            var game = games.First();

            Assert.Null(game.Event);
            Assert.Null(game.Site);
            Assert.Null(game.Round);
            Assert.Null(game.White);
            Assert.Null(game.Black);
        }

        private void AssertGame(Entities.Game game, string expected_result, string expected_algebraic_notation)
        {
            Assert.Equal(game.Result, expected_result);
            Assert.Equal(game.Moves, expected_algebraic_notation);
        }
    }
}