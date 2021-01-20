using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IReportGenerator
    {
        Task<int> Schedule(Report report, short engineDepth);
    }
}