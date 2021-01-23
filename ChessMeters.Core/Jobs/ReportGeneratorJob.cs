using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Threading.Tasks;

namespace ChessMeters.Core.Jobs
{
    public class ReportGeneratorJob : IJob
    {
        private readonly IReportGenerator reportGenerator;
        private readonly IHubContext<NotificationHub> hubContext;

        public ReportGeneratorJob(IReportGenerator reportGenerator, IHubContext<NotificationHub> hubContext)
        {
            this.reportGenerator = reportGenerator;
            this.hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            var reportId = dataMap.GetIntValue("id");
            var report = await reportGenerator.Schedule(reportId, 10);
            //await hubContext.Clients.User(report.UserId).SendAsync("reportGenerated", reportId);
            await hubContext.Clients.All.SendAsync("reportGenerated", reportId);
        }
    }
}
