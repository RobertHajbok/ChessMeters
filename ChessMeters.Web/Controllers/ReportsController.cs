using ChessMeters.Core;
using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
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
    public class ReportsController : ControllerBase
    {
        private readonly ChessMetersContext chessMetersContext;
        private readonly IReportGenerator reportGenerator;

        public ReportsController(ChessMetersContext chessMetersContext, IReportGenerator reportGenerator)
        {
            this.chessMetersContext = chessMetersContext;
            this.reportGenerator = reportGenerator;
        }

        [HttpGet]
        public async Task<IEnumerable<ReportViewModel>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await chessMetersContext.Reports.Where(x => x.UserId == userId)
                .OrderBy(x => x.Id).Select(x => new ReportViewModel
                {
                    Description = x.Description,
                    PGN = x.PGN,
                    CreationDate = x.CreationDate
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] GenerateReportViewModel generateReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = new Report
            {
                Description = generateReport.Description,
                PGN = generateReport.PGN,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                CreationDate = DateTime.Now
            };

            await chessMetersContext.Reports.AddAsync(report);
            await chessMetersContext.SaveChangesAsync();
            await reportGenerator.Schedule(report, 10);
            return Ok();
        }
    }
}
