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
        private readonly IBoardState boardState;
        private readonly IAssemblyLoader assemblyLoader;
        private readonly ChessMetersContext chessMetersContext;

        public FlagBuilder(IBoardState boardState, IAssemblyLoader assemblyLoader, ChessMetersContext chessMetersContext)
        {
            this.boardState = boardState;
            this.assemblyLoader = assemblyLoader;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task AnalizeGame(Game game)
        {
            var lastTreeMove = await chessMetersContext.TreeMoves.FindAsync(game.LastTreeMoveId);
            var pathIds = game.LastTreeMove.FullPath.Split(' ');
            var moveIds = pathIds.Select(x => long.Parse(x)).ToList();
            moveIds.Add(game.LastTreeMoveId.Value);
            var moves = await chessMetersContext.TreeMoves.Include(x => x.EngineEvaluations).Include(x => x.TreeMoveFlags)
                .Where(x => moveIds.Contains(x.Id)).ToListAsync();

            var treeMoves = new List<TreeMove>();
            foreach (var moveId in moveIds)
            {
                treeMoves.Add(moves.Single(x => x.Id == moveId));
            }

            boardState.Initialize(treeMoves, game.UserColorId);
            var rules = assemblyLoader.GetAllTypesOf<IRule>();
            var gameFlags = new HashSet<FlagEnum>();

            foreach (var treeMove in treeMoves)
            {
                var treeMoveFlags = new HashSet<FlagEnum>();
                foreach (var rule in rules)
                {
                    if (!rule.Evaluate(boardState))
                        continue;

                    if (rule.IsGameRule)
                        gameFlags.Add(rule.Flag);
                    else
                        treeMoveFlags.Add(rule.Flag);
                }

                chessMetersContext.TreeMoveFlags.RemoveRange(treeMove.TreeMoveFlags.Where(x => !treeMoveFlags.Contains(x.FlagId)));
                await chessMetersContext.TreeMoveFlags.AddRangeAsync(treeMoveFlags.Where(x => !treeMove.TreeMoveFlags.Any(tmf => tmf.FlagId == x)).Select(x => new TreeMoveFlag
                {
                    TreeMoveId = treeMove.Id,
                    FlagId = x
                }));

                boardState.SetNextTreeMove();
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