using ChessMeters.Core.Entities;

using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ChessMeters.Core.Database;
using System.IO;

namespace ChessMeters.Core.Coach
{
    public class CoachBoardStockfish : ICoachBoardEngineEvaluations
    {
        private readonly ChessMetersContext chessMetersContext;
        private Dictionary<int, int> stockfishPlyToCentipawns = new Dictionary<int, int>();

        public CoachBoardStockfish(ChessMetersContext chessMetersContext, Game game)
        {
            this.chessMetersContext = chessMetersContext;
            stockfishPlyToCentipawns = GetPlyToCentipawsMappingFromGame(game);
        }
        public int GetCentipawnsForPlyNumber(int ply_number)
        {
            return stockfishPlyToCentipawns[ply_number];
        }

        private Dictionary<int, int> GetPlyToCentipawsMappingFromGame(Game game)
        {
            Dictionary<int, int> stockfish_evaluations = new Dictionary<int, int>();

            var current_ply = game.LastTreeMove;
            while (current_ply != null)
            {
                var centipawns = current_ply.EngineEvaluations.First().EvaluationCentipawns;

                var current_ply_number = current_ply.FullPath == null
                    ? 1
                    : current_ply.FullPath.Split(" ").Count() + 1;

                stockfish_evaluations.Add(current_ply_number, centipawns);
                current_ply = current_ply.ParentTreeMove;
            }

            return stockfish_evaluations;
        }

    }

}