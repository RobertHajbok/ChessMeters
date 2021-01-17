using ChessMeters.Core.Database;
using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessMeters.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ChessMetersContext chessMetersContext;

        public ReportsController(ChessMetersContext chessMetersContext)
        {
            this.chessMetersContext = chessMetersContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ReportViewModel>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await chessMetersContext.Reports.Where(x => x.UserId == userId)
                .OrderBy(x => x.Id).Select(x => new ReportViewModel
                {
                    Description = x.Description,
                    CreationDate = x.CreationDate
                }).ToListAsync();
        }
    }
}
