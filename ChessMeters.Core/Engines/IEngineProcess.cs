using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public interface IEngineProcess
    {
        void Initialize(string path);

        void Start();

        void Wait(int millisecond);

        Task<string> ReadLine();

        Task WriteLine(string command);
    }
}