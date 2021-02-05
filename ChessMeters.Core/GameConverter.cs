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
        private List<Game> games;
        private Game currentGame;

        public async Task<IEnumerable<Game>> ConvertFromPGN(string pgn)
        {
            games = new List<Game>();
            currentGame = new Game();

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
            process.OutputDataReceived += new DataReceivedEventHandler(ParsePGN);
            process.BeginOutputReadLine();
            await process.StandardInput.WriteLineAsync(pgn);
            process.WaitForExit(1000);

            return games;
        }

        private void ParsePGN(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.EndsWith("1-0") || e.Data.EndsWith("1/2-1/2") || e.Data.EndsWith("0-1"))
            {
                currentGame.Result = e.Data.Split(' ').Last().Trim();
                currentGame.Moves = e.Data.Remove(e.Data.LastIndexOf(' ')).Trim();
                games.Add(currentGame);
                currentGame = new Game();
            }
            else
            {
                SetGamePropertyFromLine(currentGame, e.Data);
            }
        }

        public void SetGamePropertyFromLine(Game currentGame, string line)
        {
            if (line == "")
            {
                return;
            }

            var lineSplitBySpace = line.Split(" ").ToList();

            var gamePropertyKey = lineSplitBySpace.First().Replace("[", "");

            lineSplitBySpace.RemoveAt(0);
            var gamePropertyValueRaw = string.Join(" ", lineSplitBySpace);

            var gamePropertyValue = gamePropertyValueRaw.Replace("]", "").Replace("\"", "");

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
                case "WhiteElo":
                    currentGame.WhiteElo = short.Parse(gamePropertyValue);
                    break;
                case "BlackElo":
                    currentGame.BlackElo = short.Parse(gamePropertyValue);
                    break;
                case "ECO":
                    currentGame.Eco = gamePropertyValue;
                    break;
                case "ECOUrl":
                    currentGame.EcoUrl = gamePropertyValue;
                    break;
                case "TimeControl":
                    currentGame.TimeControl = gamePropertyValue;
                    break;
                case "Termination":
                    currentGame.Termination = gamePropertyValue;
                    break;
                case "Date":
                    DateTime dateValue;
                    if (DateTime.TryParse(gamePropertyValue, out dateValue))
                    {
                        currentGame.Date = dateValue;
                    }
                    break;
                case "EndTime":
                    TimeSpan endTimeValue;
                    if (TimeSpan.TryParse(gamePropertyValue.Split(" ").First(), out endTimeValue))
                    {
                        currentGame.EndTime = endTimeValue;
                    }
                    break;
                case "UTCDate":
                    DateTime utcDateValue;
                    if (DateTime.TryParse(gamePropertyValue, out utcDateValue))
                    {
                        currentGame.UTCDate = utcDateValue;
                    }
                    break;
                case "UTCTime":
                    TimeSpan utcTimeValue;
                    if (TimeSpan.TryParse(gamePropertyValue.Split(" ").First(), out utcTimeValue))
                    {
                        currentGame.UTCTime = utcTimeValue;
                    }
                    break;
                case "WhiteRatingDiff":
                    currentGame.WhiteRatingDiff = gamePropertyValue;
                    break;
                case "BlackRatingDiff":
                    currentGame.BlackRatingDiff = gamePropertyValue;
                    break;
                case "Variant":
                    currentGame.Variant = gamePropertyValue;
                    break;
                case "CurrentPosition":
                    currentGame.CurrentPosition = gamePropertyValue;
                    break;
                case "Timezone":
                    currentGame.Timezone = gamePropertyValue;
                    break;
                case "UtartTime":
                    TimeSpan startTimeValue;
                    if (TimeSpan.TryParse(gamePropertyValue.Split(" ").First(), out startTimeValue))
                    {
                        currentGame.StartTime = startTimeValue;
                    }
                    break;
                case "EndDate":
                    DateTime endDateValue;
                    if (DateTime.TryParse(gamePropertyValue, out endDateValue))
                    {
                        currentGame.EndDate = endDateValue;
                    }
                    break;
                case "Link":
                    currentGame.Link = gamePropertyValue;
                    break;
            }
        }
    }
}