using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public class StockfishEngine : IEngine
    {
        private readonly IEngineProcess engineProcess;
        private readonly int depth;
        private const int MAX_TRIES = 200;

        public StockfishEngine(IEngineProcess engineProcess, int depth = 2)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotImplementedException("Stockfish not supported.");

            this.engineProcess = engineProcess;
            this.depth = depth;
        }

        public async Task Initialize()
        {
            var exe = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "StockfishLinux" : "StockfishWindows.exe";
            engineProcess.Initialize(Path.Combine(Directory.GetCurrentDirectory(), "Resources", exe));
            engineProcess.Start();
            await engineProcess.ReadLine();
            await StartNewGame();
        }

        private async Task Send(string command, int estimatedTime = 100)
        {
            await engineProcess.WriteLine(command);
            engineProcess.Wait(estimatedTime);
        }

        public async Task StartNewGame()
        {
            await Send("ucinewgame");
            if (!await IsReady())
            {
                throw new ApplicationException();
            }
        }

        public async Task SetPosition(params string[] moves)
        {
            await StartNewGame();
            await Send($"position startpos moves {string.Join(" ", moves)}");
        }

        public async Task<string> AnalyzePosition()
        {
            var tries = 0;
            var data = string.Empty;
            await Send($"go depth {depth}");

            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new Exception();
                }

                var newLine = await engineProcess.ReadLine();
                data += newLine;
                if (newLine.StartsWith("bestmove"))
                {
                    return data;
                }

                data += Environment.NewLine;
                tries++;
            }
        }

        private async Task<bool> IsReady()
        {
            await Send("isready");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new Exception();
                }

                return await engineProcess.ReadLine() == "readyok";
            }
        }
    }
}
