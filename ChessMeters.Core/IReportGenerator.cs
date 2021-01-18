using ChessMeters.Core.Entities;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IReportGenerator
    {
        Task Schedule(Report report, string pgn);
    }
}