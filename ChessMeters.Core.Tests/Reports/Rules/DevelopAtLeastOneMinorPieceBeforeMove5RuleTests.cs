using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System.Collections.Generic;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class DevelopAtLeastOneMinorPieceBeforeMove5RuleTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMoveOneIsLessThanFive()
        {
            var blunderRule = new DevelopAtLeastOneMinorPieceBeforeMove5Rule();
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
            var blunderRule = new DevelopAtLeastOneMinorPieceBeforeMove5Rule();
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
