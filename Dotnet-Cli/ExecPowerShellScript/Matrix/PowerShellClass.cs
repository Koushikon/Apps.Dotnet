using System.Management.Automation;

namespace Matrix;

public class PowerShellClass
{
    public bool ExecuteScript(string scriptPath)
    {
        scriptPath = @"C:\file-1.ps1";
        using var ps = PowerShell.Create();
        ps.AddScript(scriptPath).Invoke();

        return !ps.HadErrors;
    }

    public string ExecuteCommand(string command)
    {
        using var ps = PowerShell.Create();
        ps.AddCommand(command);

        var process = ps.Invoke();

        return process.First().ToString();
    }

    public bool StartProcess(string processName)
    {
        using var ps = PowerShell.Create();
        ps.AddCommand("Start-Process").AddArgument(processName);
        ps.Invoke();

        return !ps.HadErrors;
    }
}