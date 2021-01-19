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
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<ReportViewModel>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await chessMetersContext.Reports.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreationDate).Select(x => new ReportViewModel
                {
                    Id = x.Id,
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

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditReportViewModel editReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await chessMetersContext.Reports.SingleAsync(x => x.Id == editReport.Id);
            report.Description = editReport.Description;
            report.LastUpdateUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            report.LastUpdated = DateTime.Now;

            chessMetersContext.Reports.Update(report);
            await chessMetersContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("GetForEdit/{id:int}")]
        public async Task<EditReportViewModel> GetForEdit(int id)
        {
            return await chessMetersContext.Reports.Select(x => new EditReportViewModel
            {
                Id = x.Id,
                Description = x.Description
            }).SingleAsync(x => x.Id == id);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await chessMetersContext.Reports.Include(x => x.Games).SingleAsync(x => x.Id == id);
            chessMetersContext.Games.RemoveRange(report.Games);
            chessMetersContext.Reports.Remove(report);
            await chessMetersContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
