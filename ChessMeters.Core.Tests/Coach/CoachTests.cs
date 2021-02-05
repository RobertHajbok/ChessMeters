using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ChessMeters.Core.Coach.Tests
{
    public class CoachTests
    {
        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_1()
        {
            // white castled on move 8
            // black castled on move 11
            var algebraic = "e2e4 c7c5 g1f3 e7e6 b1c3 a7a6 d2d4 c5d4 f3d4 d8c7 f1e2 f8b4 c1d2 g8f6 e1g1 b4c3 d2c3 f6e4 d1d3 e4c3 b2c3 e8g8 e2f3 b8c6 a1b1 d7d5 f1e1 c6e5 d3e2 e5f3 e2f3 b7b5 e1e3 c8b7 f3g4 f7f5 g4g5 h7h6 g5g6 f8f6 g6h5 e6e5 d4f5 a8f8 f5h4 f6f2 e3e5 c7c3 e5f5 c3c2 f5f8 f2f8 b1f1 f8f1 g1f1 d5d4 h5e8 g8h7 e8d7 d4d3 d7f5 h7g8 f5e6 g8h7 e6f5 h7g8 f5e6 g8f8 h4g6";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleCastling()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            // White castled on move 8, before move 10, should not flag anything.
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black castled on move 11, after move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_2()
        {
            // white castled on move 10
            // black castled on move 17                                                                                 x                                                                          x
            var algebraic = "e2e4 c7c5 g1f3 e7e6 f1b5 a7a6 b5c4 b7b5 c4e2 d8c7 e4e5 b8c6 b1c3 c6e5 d2d4 e5f3 e2f3 d7d5 e1g1 c5d4 c3e2 f8d6 g2g3 f7f6 e2d4 e6e5 d4e2 e5e4 f3g2 g8e7 a2a4 b5b4 a4a5 e8g8 c1e3 d6c5 e3f4 c5d6 f4d6 c7d6 e2f4 e7c6 f4d5 f6f5 d5e3 a8b8 d1d6	";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleCastling()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            // White castled on move 10, before move 10, should not flag anything.
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black castled on move 17, after move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Castle_Case_3()
        {
            // white did not castle
            // black did not castle                                                                                                                                                      
            var algebraic = "e2e4 e7e5 f1c4 g8f6 d2d3 h7h6 f2f4 d7d6 g1f3 c8g4 h2h3 g4h5 g2g4 h5g6 g4g5 f6d7 f4f5 g6h7 g5g6 f7g6 f5g6 h7g6 h1f1 g6h5 d1d2 b8c6 b1c3 d8f6 c3d5 f6d8 b2b4 h5f3 f1f3 c6d4 f3f2 c7c6 d5e3 d8h4 c4f7 e8d8 d2d1 h4h3 e3f5 h3h1 e1d2 h1d1 d2d1 d7f6 c1b2 f6g4 f2d2 d4f5 e4f5 g4e3 d1e2 e3f5 e2f3 f8e7 f7e6 f5d4 b2d4 e5d4 f3e4 e7g5 d2f2 d8e7 f2f7 e7e6 a1f1 d6d5";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleCastling()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            // White did not castle before move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 0);

            // Black did not castle before move 10, should be flagged.
            AssertFlagsContain(flags, typeof(CoachFlagDidNotCastle), 1);
        }

        private void AssertFlagsContain(List<ICoachFlag> flags, Type expectedFlagType, int playerColor)
        {
            Assert.True(
                FlagExistsForColor(flags, expectedFlagType, playerColor)
            );
        }

        private void AssertFlagsNotContain(List<ICoachFlag> flags, Type expectedFlagType, int playerColor)
        {
            Assert.True(
                !FlagExistsForColor(flags, expectedFlagType, playerColor)
            );
        }

        private bool FlagExistsForColor(List<ICoachFlag> flags, Type expectedFlagType, int playerColor)
        {
            return flags.Exists(
                flag => (flag.GetType() == expectedFlagType) && (flag.GetPlayerColor() == playerColor)
            );
        }
    }
}
