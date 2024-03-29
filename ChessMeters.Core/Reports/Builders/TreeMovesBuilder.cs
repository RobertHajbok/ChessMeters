﻿using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using ChessMeters.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public class TreeMovesBuilder : ITreeMovesBuilder
    {
        private readonly ChessMetersContext chessMetersContext;
        private readonly IEngineEvaluationBuilder engineAnalyzeEvaluator;

        public TreeMovesBuilder(ChessMetersContext chessMetersContext, IEngineEvaluationBuilder engineAnalyzeEvaluator)
        {
            this.chessMetersContext = chessMetersContext;
            this.engineAnalyzeEvaluator = engineAnalyzeEvaluator;
        }

        public async Task<IEnumerable<TreeMove>> BuildTree(short analyzeDepth, Game game)
        {
            var treeMoves = new List<TreeMove>();
            try
            {
                var moves = game.Moves.Split(' ');
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
                            ParentTreeMove = parentTreeMove,
                            ColorId = fullPathIds.Count % 2 == 0 ? ColorEnum.White : ColorEnum.Black
                        };
                        await chessMetersContext.TreeMoves.AddAsync(treeMove);
                        await chessMetersContext.SaveChangesAsync();
                    }

                    await engineAnalyzeEvaluator.BuildEngineEvaluations(treeMove, treeMoves.Select(x => x.Move).ToArray());

                    treeMoves.Add(treeMove);
                    fullPathIds.Add(treeMove.Id);
                    parentTreeMove = treeMove;
                }

                game.LastTreeMoveId = fullPathIds.LastOrDefault();
                chessMetersContext.Games.Update(game);
                await chessMetersContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                game.AnalyzeExceptionStackTrace = ex.ToString();
            }

            return treeMoves;
        }
    }
}
