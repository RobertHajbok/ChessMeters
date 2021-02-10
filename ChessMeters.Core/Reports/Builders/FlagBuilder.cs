using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using ChessMeters.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public class FlagBuilder : IFlagBuilder
    {
        private readonly IBoardState coachBoard;
        private readonly IAssemblyLoader assemblyLoader;
        private readonly ChessMetersContext chessMetersContext;

        public FlagBuilder(IBoardState coachBoard, IAssemblyLoader assemblyLoader, ChessMetersContext chessMetersContext)
        {
            this.coachBoard = coachBoard;
            this.assemblyLoader = assemblyLoader;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task AnalizeGame(Game game)
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
            var rules = assemblyLoader.GetAllTypesOf<IRule>();
            var gameFlags = new HashSet<FlagEnum>();

            foreach (var treeMove in treeMoves)
            {
                chessMetersContext.TreeMoveFlags.RemoveRange();
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

                coachBoard.SetNextTreeMove();
            }

            chessMetersContext.GameFlags.RemoveRange(game.GameFlags);
            await chessMetersContext.GameFlags.AddRangeAsync(gameFlags.Select(x => new GameFlag
            {
                FlagId = x,
                GameId = game.Id
            }));
            await chessMetersContext.SaveChangesAsync();
        }
    }
}