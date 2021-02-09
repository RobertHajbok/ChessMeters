using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core.Coach
{
    public class Coach : ICoach
    {
        private readonly ICoachBoard coachBoard;
        private readonly ChessMetersContext chessMetersContext;

        public Coach(ICoachBoard coachBoard, ChessMetersContext chessMetersContext)
        {
            this.coachBoard = coachBoard;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task AnalizeGame(Game game, IEnumerable<ICoachRule> rules)
        {
            var lastTreeMove = await chessMetersContext.TreeMoves.FindAsync(game.LastTreeMoveId);
            var pathIds = game.LastTreeMove.FullPath.Split(' ');
            var moveIds = pathIds.Select(x => long.Parse(x)).ToList();
            moveIds.Add(game.LastTreeMoveId.Value);
            var moves = await chessMetersContext.TreeMoves.Include(x => x.EngineEvaluations).Where(x => moveIds.Contains(x.Id)).ToListAsync();

            var treeMoves = new List<TreeMove>();
            foreach (var moveId in moveIds)
            {
                treeMoves.Add(moves.Single(x => x.Id == moveId));
            }

            coachBoard.Initialize(treeMoves);
            var gameFlags = new HashSet<FlagEnum>();

            foreach (var treeMove in treeMoves)
            {
                foreach (var rule in rules)
                {
                    var flag = rule.Evaluate(coachBoard);
                    if (flag == null)
                        continue;

                    if (rule.IsGameRule)
                        gameFlags.Add(flag.Value);
                    else
                    {
                        await chessMetersContext.TreeMoveFlags.AddAsync(new TreeMoveFlag
                        {
                            TreeMoveId = treeMove.Id,
                            FlagId = flag.Value
                        });
                    }
                }


                coachBoard.NextPly();
            }
        }
    }
}