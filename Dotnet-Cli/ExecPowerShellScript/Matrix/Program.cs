
using Matrix;

var file = "file-1.ps1";
var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\Files\", file);

Console.WriteLine(filePath);

/***
 * With ProcessStart class
 */
var processStart = new ProcessStart();

string output1 = processStart.ExecuteScript(filePath);
Console.WriteLine(output1);

string output2 = processStart.ExecuteCommand("echo 'Invoking the powershell command!'");
Console.WriteLine(output2);


/***
 * With PowerShellClass class
 */
var powerShellClass = new PowerShellClass();

// Its not woking in my WIndows 10
//bool output3 = powerShellClass.ExecuteScript(filePath);
//Console.WriteLine(output3);

string output4 = powerShellClass.ExecuteCommand("Get-Date");
Console.WriteLine(output4);

bool output5 = powerShellClass.StartProcess("notepad");
Console.WriteLine(output5);


/***
 * With PSCustomRunspace class
 */
var customRunspace = new PSCustomRunspace();

string output6 = customRunspace.ExecuteCommand("Get-Date");
Console.WriteLine(output6.ToString());

customRunspace.StartProcess("notepad");

bool output7 = customRunspace.StartProcess("notepad");
Console.WriteLine(output7.ToString());