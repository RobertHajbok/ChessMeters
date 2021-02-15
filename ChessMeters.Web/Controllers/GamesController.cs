using ChessMeters.Core.Database;
using ChessMeters.Core.Enums;
using ChessMeters.Core.Extensions;
using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessMeters.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ChessMetersContext chessMetersContext;

        public GamesController(ChessMetersContext chessMetersContext)
        {
            this.chessMetersContext = chessMetersContext;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GameDetailsViewModel> GetDetails(int id)
        {
            var game = await chessMetersContext.Games.Include(x => x.LastTreeMove).Include(x => x.Report)
                .Include(x => x.GameFlags).SingleAsync(x => x.Id == id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (game.Report.UserId != userId)
            {
                throw new Exception("User not authorized for this operation.");
            }

            if (!game.LastTreeMoveId.HasValue)
            {
                return new GameDetailsViewModel();
            }

            var pathIds = game.LastTreeMove.FullPath.Split(' ');
            var moveIds = pathIds.Select(x => long.Parse(x)).ToList();
            moveIds.Add(game.LastTreeMoveId.Value);
            var moves = await chessMetersContext.TreeMoves.Include(x => x.EngineEvaluations)
                .Include(x => x.TreeMoveFlags).Where(x => moveIds.Contains(x.Id)).ToListAsync();

            var treeMoves = new List<TreeMoveViewModel>();
            foreach (var move in moves)
            {
                var treeMove = moves.Single(x => x.Id == move.Id);
                treeMoves.Add(new TreeMoveViewModel
                {
                    Move = move.Move,
                    StockfishEvaluationCentipawns = move.EngineEvaluations.Single(x => x.EngineId == EngineEnum.Stockfish12).EvaluationCentipawns,
                    Flags = move.TreeMoveFlags.Select(x => x.FlagId.GetEnumDisplayUI())
                });
            }
            return new GameDetailsViewModel
            {
                TreeMoves = treeMoves,
                Event = game.Event,
                Site = game.Site,
                Round = game.Round,
                White = game.White,
                Black = game.Black,
                Result = game.Result,
                WhiteElo = game.WhiteElo,
                BlackElo = game.BlackElo,
                Eco = game.Eco,
                TimeControl = game.TimeControl,
                Termination = game.Termination,
                Date = game.Date,
                EndTime = game.EndTime,
                UTCDate = game.UTCDate,
                UTCTime = game.UTCTime,
                WhiteRatingDiff = game.WhiteRatingDiff,
                BlackRatingDiff = game.BlackRatingDiff,
                Variant = game.Variant,
                Flags = game.GameFlags.Select(x => x.FlagId.GetEnumDisplayUI())
            };
        }
    }
}
