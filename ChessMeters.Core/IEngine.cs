using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public interface IEngine
    {
        Task<string> StartAnalyse(short depth);
    }
}