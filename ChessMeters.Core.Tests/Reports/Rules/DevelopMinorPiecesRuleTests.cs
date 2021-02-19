using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System.Collections.Generic;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class DevelopMinorPiecesRuleTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMoveOneIsLessThanTwelve()
        {
            var blunderRule = new DevelopMinorPiecesRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove { ColorId = ColorEnum.White }
            }, ColorEnum.White);

            var result = blunderRule.Evaluate(boardState);

            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIMoveColorIsNotUserColor()
        {
            var blunderRule = new DevelopMinorPiecesRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove { ColorId = ColorEnum.White }
            }, ColorEnum.Black);

            var result = blunderRule.Evaluate(boardState);

            Assert.False(result);
        }
    }
}
