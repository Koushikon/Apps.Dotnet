namespace Matrix;

/***
 * Note: #0001
 * Explain: Create a Temp File in the Temp Folder in C#
 * Source: https://code-maze.com/csharp-how-to-create-a-temp-file-in-temp-folder/
 */

class Program
{
    static void Main()
    {
        // Creating a temp file using GetTempPath method
        string tempFile = Path.Combine(Path.GetTempPath(), "first.txt");
        TempFileCreator.CreateTempFile(tempFile);
        Console.WriteLine("Create the Temp file in " + tempFile);
        TempFileCreator.ReadTempFile(tempFile);


        // Create a temp file using GetTempFileName method
        tempFile = Path.GetTempFileName();
        TempFileCreator.CreateTempFile(tempFile);
        Console.WriteLine($"Temp file {tempFile} exists? {File.Exists(tempFile)}");
        TempFileCreator.ReadTempFile(tempFile);


        // Create a temp file using GetFolderPath() method
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        tempFile = Path.Combine(appDataPath, "second.txt");
        TempFileCreator.CreateTempFile(tempFile);
        Console.WriteLine($"Temp file {tempFile} exists? {File.Exists(tempFile)}");
        TempFileCreator.ReadTempFile(tempFile);

        Console.WriteLine("End of world.");
    }
}