using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System.Collections.Generic;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class CastlingRuleTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMoveOneIsLessThanTen()
        {
            var blunderRule = new CastlingRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove()
            }, ColorEnum.White);

            var result = blunderRule.Evaluate(boardState);

            Assert.False(result);
        }
    }
}
