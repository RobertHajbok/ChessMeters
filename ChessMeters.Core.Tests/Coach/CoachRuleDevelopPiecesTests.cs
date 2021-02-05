using System;
using System.Collections.Generic;
using Xunit;

namespace ChessMeters.Core.Coach.Tests
{
    public class CoachRuleDevelopPiecesTests
    {
        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_1()
        {
            // white develops normal
            // black doesn't develop any minor piece
            var algebraic = "e2e4 d7d5 e4d5 f7f6 d2d3 e7e5 g1f3 g7g6 b1c3 a7a6 f1e2 b7b6 e1g1 h7h6 c1e3 g6g5 d1d2 b6b5 a1b1 a6a5 f1e1 a5a4 g2g3 h6h5 e2f1 c7c5 f1g2 a4a3 b2b3 b5b4 c3b5 c5c4";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleDevelopMinorPiecesWhite(),
                new CoachRuleDevelopMinorPiecesBlack()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
        }

        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_2()
        {
            // white doesn't develop any minor piece
            // black develops normaal
            var algebraic = "a2a3 g8f6 b2b3 b8c6 c2c3 e7e6 d2d3 d7d6 e2e3 f8e7 f2f3 c8d7 g2g3 e8g8 h2h4 d8c8 a3a4 f8e8 b3b4 e8f8 c3c4 c8b8 d3d4 f8e8 e3e4 e8f8 f3f4 b8c8 g3g4 f8d8 h4h5";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleDevelopMinorPiecesWhite(),
                new CoachRuleDevelopMinorPiecesBlack()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
        }

        [Fact]
        public void AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_Pieces_Case_3()
        {
            // white develops normal
            // black doesn't develop one knight
            var algebraic = "e2e4 c7c5 g1f3 e7e6 d2d4 c5d4 f3d4 a7a6 b1c3 d8c7 f1d3 b8c6 e1g1 b7b5 c1e3 c8b7 f2f4 h7h6 g1h1 f8b4 a2a3 b4c5 f1f3 c5d4 e3d4 c6d4 d3b5 a6b5 d1d4";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>
            {
                new CoachRuleDevelopMinorPiecesWhite(),
                new CoachRuleDevelopMinorPiecesBlack()
            };

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 0);
            AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAllPieces), 1);
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