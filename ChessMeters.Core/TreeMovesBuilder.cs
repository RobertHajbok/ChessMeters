﻿using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class TreeMovesBuilder : ITreeMovesBuilder
    {
        private readonly ChessMetersContext chessMetersContext;
        private readonly IEngineAnalyzeEvaluator engineAnalyzeEvaluator;

        public TreeMovesBuilder(ChessMetersContext chessMetersContext, IEngineAnalyzeEvaluator engineAnalyzeEvaluator)
        {
            this.chessMetersContext = chessMetersContext;
            this.engineAnalyzeEvaluator = engineAnalyzeEvaluator;
        }

        public async Task<IEnumerable<TreeMove>> BuildTree(short analyzeDepth, params string[] moves)
        {
            var treeMoves = new List<TreeMove>();
            if (!moves.Any())
                return treeMoves;

            var fullPathIds = new List<long>();
            TreeMove parentTreeMove = null;

            await engineAnalyzeEvaluator.StartNewGame(analyzeDepth);

            foreach (var move in moves)
            {
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

                await engineAnalyzeEvaluator.BuildEngineEvaluations(treeMove);

                treeMoves.Add(treeMove);
                fullPathIds.Add(treeMove.Id);
                parentTreeMove = treeMove;
            }

            return treeMoves;
        }
    }
}