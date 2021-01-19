﻿using ChessMeters.Core.Engines.Enums;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public class StockfishEngine : IEngine
    {
        private readonly IEngineProcess engineProcess;
        private const int MAX_TRIES = 200;
        private short depth;

        public EngineEnum EngineId { get; }

        public StockfishEngine(IEngineProcess engineProcess)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotImplementedException("Stockfish not supported.");

            this.engineProcess = engineProcess;
            EngineId = EngineEnum.Stockfish12;
        }

        public async Task Initialize(short depth = 2)
        {
            this.depth = depth;
            // var exe = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "StockfishLinux" : "StockfishWindows.exe";
            // engineProcess.Initialize(Path.Combine(Directory.GetCurrentDirectory(), "Resources", exe));
            engineProcess.Initialize("/home/claudiuoprea/ChessMeters/ChessMeters.Web/bin/Debug/net5.0/Resources/StockfishLinux");
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

        public async Task<short> GetEvaluationCentipawns()
        {
            var data = await AnalyzePosition();
            var search = $"info depth {depth} ";
            var currentDepthData = data[(data.IndexOf(search) + search.Length)..]; //"info depth 20 seldepth 24 multipv 1 score cp -25";
            return short.Parse(currentDepthData[(currentDepthData.IndexOf(" cp ") + 4)..].Split(' ')[0]);
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
