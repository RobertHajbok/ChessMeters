using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

using ChessMeters.Core.Coach;

namespace ChessMeters.Core.Coach.Tests
{
    public class CoachRuleDevelopAtLeastOneMinorPieceBeforeMove5Tests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_At_Least_1_Minor_Before_M5_1()
        {
            // white develops normal
            // black doesn't develop any minor piece
            var algebraic = "e2e4 d7d5 e4d5 f7f6 d2d3 e7e5 g1f3 g7g6 b1c3 a7a6 f1e2 b7b6 e1g1 h7h6 c1e3 g6g5 d1d2 b6b5 a1b1 a6a5 f1e1 a5a4 g2g3 h6h5 e2f1 c7c5 f1g2 a4a3 b2b3 b5b4 c3b5 c5c4";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>();
            rules.Add(new CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5White());
            rules.Add(new CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5Black());

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5), 0);
            AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5), 1);
        }

        public async Task AnalyzeGame_Should_Analyze_A_Basic_Game_Develop_At_Least_1_Minor_Before_M5_2()
        {
            // white doesn't develop any minor piece
            // black develops normaal
            var algebraic = "a2a3 g8f6 b2b3 b8c6 c2c3 e7e6 d2d3 d7d6 e2e3 f8e7 f2f3 c8d7 g2g3 e8g8 h2h4 d8c8 a3a4 f8e8 b3b4 e8f8 c3c4 c8b8 d3d4 f8e8 e3e4 e8f8 f3f4 b8c8 g3g4 f8d8 h4h5";
            var board = new CoachBoard(algebraic);
            var rules = new List<ICoachRule>();
            rules.Add(new CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5White());
            rules.Add(new CoachRuleDevelopAtLeaastOneMinorPieceBeforeMove5Black());

            var coach = new Coach(algebraic, board, rules);
            var flags = coach.AnalizeGame();

            AssertFlagsContain(flags, typeof(CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5), 0);
            AssertFlagsNotContain(flags, typeof(CoachFlagDidNotDevelopAtLeastOneMinorPieceBeforeMove5), 1);
        }

        private void AssertFlagsContain(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            Assert.True(
                FlagExistsForColor(flags, expected_flag_type, player_color)
            );
        }

        private void AssertFlagsNotContain(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            Assert.True(
                !FlagExistsForColor(flags, expected_flag_type, player_color)
            );
        }

        private bool FlagExistsForColor(List<ICoachFlag> flags, System.Type expected_flag_type, int player_color)
        {
            return flags.Exists(
                flag => (flag.GetType() == expected_flag_type) && (flag.GetPlayerColor() == player_color)
            );
        }
    }
}