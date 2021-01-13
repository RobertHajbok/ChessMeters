﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChessMeters.Core
{
    public class StockfishEngine : IEngine
    {
        public async Task<string> StartAnalyse(short depth)
        {

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotImplementedException("Stockfish not supported.");

	    var exe = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "StockfishLinux" : "StockfishWindows.exe";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", exe),
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = $"go depth {depth}"
                }
            };

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            process.Close();

            return output;
        }
    }
}
