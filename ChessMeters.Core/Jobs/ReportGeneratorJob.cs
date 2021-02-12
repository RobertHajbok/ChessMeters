using ChessMeters.Core.Database;
using ChessMeters.Core.Dtos;
using ChessMeters.Core.Reports;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Threading.Tasks;

namespace ChessMeters.Core.Jobs
{
    public class ReportGeneratorJob : IJob
    {
        private readonly IReportGenerator reportGenerator;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly ChessMetersContext chessMetersContext;

        public ReportGeneratorJob(IReportGenerator reportGenerator, IHubContext<NotificationHub> hubContext,
            ChessMetersContext chessMetersContext)
        {
            this.reportGenerator = reportGenerator;
            this.hubContext = hubContext;
            this.chessMetersContext = chessMetersContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            var reportId = dataMap.GetIntValue("id");
            var report = await chessMetersContext.Reports.Include(x => x.Games).ThenInclude(x => x.GameFlags)
                .SingleAsync(x => x.Id == reportId);

            var analyzedGames = 0;
            var analyzeErrorGames = 0;
            foreach (var game in report.Games)
            {
                await reportGenerator.Schedule(game, 10);
                if (game.Analyzed)
                    analyzedGames++;
                else
                    analyzeErrorGames++;
                await hubContext.Clients.User(report.UserId).SendAsync("reportGameAnalyzed", new ReportGameAnalyzedDto(reportId, analyzedGames, analyzeErrorGames));
            }
            await hubContext.Clients.User(report.UserId).SendAsync("reportGenerated", reportId);
        }
    }
}
