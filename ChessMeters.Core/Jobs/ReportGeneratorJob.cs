using Quartz;
using System.Threading.Tasks;

namespace ChessMeters.Core.Jobs
{
    public class ReportGeneratorJob : IJob
    {
        private readonly IReportGenerator reportGenerator;

        public ReportGeneratorJob(IReportGenerator reportGenerator)
        {
            this.reportGenerator = reportGenerator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            var reportId = dataMap.GetIntValue("id");
            await reportGenerator.Schedule(reportId, 10);
        }
    }
}
