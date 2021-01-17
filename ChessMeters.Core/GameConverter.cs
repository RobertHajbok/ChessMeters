using ChessMeters.Core.Entities;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class GameConverter : IGameConverter
    {
        public async Task<Game> ConvertFromPGN(string pgn)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotImplementedException("Conversion not supported.");

            var exe = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "pgn-extract-linux.sh" : "pgn-extract-windows.exe";
            var processStartInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", exe),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = "-Wuci"
            };
            using var process = new Process { StartInfo = processStartInfo };
            process.Start();
            await process.StandardInput.WriteLineAsync(pgn);
            await process.StandardInput.FlushAsync();

            var game = new Game
            {
                Result = pgn.Split(' ').Last().Trim()
            };

            string output = string.Empty;
            string line;
            while ((line = await process.StandardOutput.ReadLineAsync()) != null)
            {
                output += line;
                if (line.EndsWith("1-0") || line.EndsWith("1/2-1/2") || line.EndsWith("0-1"))
                {
                    game.Moves = line.Remove(line.LastIndexOf(' ')).Trim();
                    break;
                }
                else
                {
                    output += Environment.NewLine;
                }
            }
            process.WaitForExit(1000);

            return game;
        }
    }
}