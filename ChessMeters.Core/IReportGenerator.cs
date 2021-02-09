using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IReportGenerator
    {
        Task<Report> Schedule(int reportId, short engineDepth);
    }
}