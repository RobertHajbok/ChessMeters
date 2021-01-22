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

            var currentGame = new Game();
            while ((line = await process.StandardOutput.ReadLineAsync()) != null)
            {
                if (line == endOfGamesDelimiterAsLAN)
                {
                    break;
                }
                output += line;
                if (line.EndsWith("1-0") || line.EndsWith("1/2-1/2") || line.EndsWith("0-1"))
                {
                    currentGame.Result = line.Split(' ').Last().Trim();
                    currentGame.Moves = line.Remove(line.LastIndexOf(' ')).Trim();
                    games.Add(currentGame);
                    currentGame = new Game();
                }
                else
                {
                    SetGamePropertyFromLine(currentGame, line);
                    output += Environment.NewLine;
                }
            }
            process.WaitForExit(1000);

            return games;
        }
        public void SetGamePropertyFromLine(Game currentGame, string line)
        {
            if (line == "")
            {
                return;
            }

            var gamePropertyKey = line.Split(" ").First().Replace("[", "");
            var gamePropertyValue = line.Split(" ").
              ElementAt(1).
              Replace("]", "").
              Replace("\"", "");
            
            if (gamePropertyValue == "?")
            {
                return;
            }
            
            switch (gamePropertyKey)
            {
                case "Event":
                    currentGame.Event = gamePropertyValue;
                    break;
                case "Site":
                    currentGame.Site = gamePropertyValue;
                    break;
                case "Round":
                    currentGame.Round = gamePropertyValue;
                    break;
                case "White":
                    currentGame.White = gamePropertyValue;
                    break;
                case "Black":
                    currentGame.Black = gamePropertyValue;
                    break;
                default:
                    break;
            }
        }

    }
}