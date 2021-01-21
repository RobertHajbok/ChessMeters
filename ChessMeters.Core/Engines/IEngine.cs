using ChessMeters.Core.Engines.Enums;
using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public interface IEngine
    {
        EngineEnum EngineId { get; }

        Task Initialize(short depth = 2);

        Task SetPosition(params string[] move);

        Task<string> AnalyzePosition();

        Task<short> GetEvaluationCentipawns(bool color);
    }
}