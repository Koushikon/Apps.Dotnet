namespace Matrix;

public static class TempFileCreator
{
    public static void CreateTempFile(string filePath)
    {
        using (var sw = new StreamWriter(filePath))
        {
            sw.Write("The JavaScript reference serves as a repository of facts about the JavaScript language.");
            sw.Close();
        }
    }

    public static void ReadTempFile(string filePath)
    {
        Console.WriteLine("Reading the temp file contents " + filePath);
        string tempFileContent = File.ReadAllText(filePath);
        Console.WriteLine("Content: " + tempFileContent);
    }
}