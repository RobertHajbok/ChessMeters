using ChessMeters.Core.Database;
using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var game = await chessMetersContext.Games.SingleAsync(x => x.Id == id);
            return new GameDetailsViewModel();
        }
    }
}
