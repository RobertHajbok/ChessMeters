using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChessMeters.Core.Engines
{
    public class EngineProcess : IEngineProcess
    {
        private ProcessStartInfo processStartInfo;
        private Process process;

        public void Initialize(string path)
        {
            processStartInfo = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            process = new Process { StartInfo = processStartInfo };
        }

        public void Wait(int millisecond)
        {
            process.WaitForExit(millisecond);
        }

        public async Task WriteLine(string command)
        {
            if (process.StandardInput == null)
            {
                throw new NullReferenceException();
            }
            await process.StandardInput.WriteLineAsync(command);
            await process.StandardInput.FlushAsync();
        }

        public async Task<string> ReadLine()
        {
            if (process.StandardOutput == null)
            {
                throw new NullReferenceException();
            }
            return await process.StandardOutput.ReadLineAsync();
        }

        public void Start()
        {
            process.Start();
        }

        ~EngineProcess()
        {
            process.Close();
        }
    }
}
