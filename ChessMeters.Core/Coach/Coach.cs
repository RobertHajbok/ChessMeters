using System.Collections.Generic;

namespace ChessMeters.Core.Coach
{
    public class Coach : ICoach
    {
        private ICoachBoard board;
        private List<ICoachRule> rules;
        private List<ICoachFlag> flags = new List<ICoachFlag>();

        public Coach(string pgn, ICoachBoard board, List<ICoachRule> rules)
        {
            this.board = board;
            this.rules = rules;
        }

        public List<ICoachFlag> AnalizeGame()
        {
            while (board.HasNextPly())
            {
                AnalizeCurrentPosition();
                board.NextPly();
            }
            return flags;
        }

        private void AnalizeCurrentPosition()
        {
            foreach (var rule in rules)
            {
                var flag = rule.Evaluate(this.board);
                if (flag != null)
                {
                    flags.Add(flag);
                }
            }
        }
    }

}