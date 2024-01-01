using System.Diagnostics;

namespace Matrix;

public class ProcessStart
{
    public string ExecuteScript(string scriptPath)
    {
        // -ExecutionPolicy This Bypass the Execution policy of script so we can run the script
        string scriptArguments = "-ExecutionPolicy Bypass -File " + scriptPath;        

        var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        Console.WriteLine(error);

        return output;
    }

    public string ExecuteCommand(string command)
    {
        var processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "powershell.exe";
        processStartInfo.Arguments = $"-Command \"{command}\"";
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;

        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();

        return output;
    }
}