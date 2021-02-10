using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core.Reports
{
    public interface IReportGenerator
    {
        Task<Report> Schedule(int reportId, short engineDepth);
    }
}