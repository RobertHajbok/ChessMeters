using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public interface IEngine
    {
        Task Initialize();

        Task SetPosition(params string[] move);

        Task<string> AnalyzePosition();
    }
}