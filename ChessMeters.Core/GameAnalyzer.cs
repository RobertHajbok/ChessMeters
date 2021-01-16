using ChessMeters.Core.Database;
using ChessMeters.Core.Engines;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class GameAnalyzer
    {
        private readonly IEngine engine;
        private readonly ChessMetersContext chessMetersContext;

        public GameAnalyzer(IEngine engine, ChessMetersContext chessMetersContext)
        {
            this.engine = engine;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task<IEnumerable<TreeMove>> AnalizeGame(params string[] moves)
        {
            var treeMoves = new List<TreeMove>();
            if (!moves.Any())
                return treeMoves;

            var fullPathIds = new List<long>();
            TreeMove parentTreeMove = null;
            //await engine.Initialize();

            for (var i = 0; i < moves.Length; i++)
            {
                //await engine.SetPosition(string.Join(" ", moves.Take(i + 1)));
                //var uu = await engine.AnalyzePosition();

                var move = moves[i];
                var parentTreeMoveId = parentTreeMove?.Id;
                var treeMove = await chessMetersContext.TreeMoves.SingleOrDefaultAsync(x => x.Move == move && x.ParentTreeMoveId == parentTreeMoveId);

                if (treeMove == null)
                {
                    treeMove = new TreeMove
                    {
                        Move = move,
                        FullPath = fullPathIds.Any() ? string.Join(" ", fullPathIds) : null,
                        ParentTreeMoveId = parentTreeMoveId,
                        ParentTreeMove = parentTreeMove
                    };
                    await chessMetersContext.TreeMoves.AddAsync(treeMove);
                    await chessMetersContext.SaveChangesAsync();
                }

                treeMoves.Add(treeMove);
                fullPathIds.Add(treeMove.Id);
                parentTreeMove = treeMove;
            }

            return treeMoves;
        }
    }
}
