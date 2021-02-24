using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using System.Collections.Generic;
using Xunit;

namespace ChessMeters.Core.Reports.Tests
{
    public class MistakeRuleTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMoveOneIsOne()
        {
            var mistakeRule = new MistakeRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove()
            }, ColorEnum.White);

            var result = mistakeRule.Evaluate(boardState);

            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMistakeThresholdIsNotMet()
        {
            var mistakeRule = new MistakeRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = 0 }
                    },
                    Move = "d2d4"
                },
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10 11",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = RuleConsts.mistakeThreshold - 1 }
                    },
                    Move = "f8e7"
                }
            }, ColorEnum.White);
            boardState.SetNextTreeMove();

            var result = mistakeRule.Evaluate(boardState);

            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnTrueIfMistakeThresholdIsMetAndBlunderThresholdIsNotMet()
        {
            var mistakeRule = new MistakeRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = 0 }
                    },
                    Move = "d2d4"
                },
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10 11",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = RuleConsts.mistakeThreshold }
                    },
                    Move = "f8e7"
                }
            }, ColorEnum.White);
            boardState.SetNextTreeMove();

            var result = mistakeRule.Evaluate(boardState);

            Assert.True(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Evaluate_Should_ReturnFalseIfMistakeThresholdIsMetAndBlunderThresholdIsAlsoMet()
        {
            var mistakeRule = new MistakeRule();
            var boardState = new BoardState();
            boardState.Initialize(new List<TreeMove>
            {
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = 0 }
                    },
                    Move = "d2d4"
                },
                new TreeMove
                {
                    FullPath = "1 2 3 4 5 6 7 8 9 10 11",
                    EngineEvaluations = new List<EngineEvaluation>
                    {
                        new EngineEvaluation { EvaluationCentipawns = RuleConsts.blunderThreshold }
                    },
                    Move = "f8e7"
                }
            }, ColorEnum.White);
            boardState.SetNextTreeMove();

            var result = mistakeRule.Evaluate(boardState);

            Assert.False(result);
        }
    }
}
