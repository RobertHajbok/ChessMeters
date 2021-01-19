using ChessMeters.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class GameConverter : IGameConverter
    {
        private const string endOfGamesDelimiterAsPGN = "1. Nc3 Nc6 2. Nb1 Nb8 1-0";
        private const string endOfGamesDelimiterAsLAN = "b1c3 b8c6 c3b1 c6b8 1-0";

        public async Task<IEnumerable<Game>> ConvertFromPGN(string pgn)
        {
            // HACK: required to identify last line from output
            // produced by pgn-extract while reading with ReadLineAsync.
            //
            // TODO: Find a better way to support multiple games insite
            // same pgn.
            pgn = $"{pgn}{endOfGamesDelimiterAsPGN}";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotImplementedException("Conversion not supported.");

            var exe = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "pgn-extract-linux" : "pgn-extract-windows.exe";
            var processStartInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Resources", exe),
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

            var games = new List<Game>();

            string output = string.Empty;
            string line;

            while ((line = await process.StandardOutput.ReadLineAsync()) != null)
            {
                if (line == endOfGamesDelimiterAsLAN)
                {
                    break;
                }
                output += line;
                if (line.EndsWith("1-0") || line.EndsWith("1/2-1/2") || line.EndsWith("0-1"))
                {
                    var game = new Game
                    {
                        Result = line.Split(' ').Last().Trim()
                    };
                    game.Moves = line.Remove(line.LastIndexOf(' ')).Trim();
                    games.Add(game);
                }
                else
                {
                    output += Environment.NewLine;
                }
            }
            process.WaitForExit(1000);

            return games;
        }
    }
}