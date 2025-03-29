using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public class TextFileDataAccess
{
    public List<Contact> ReadAllRecords()
    {

    }

    public void WriteAllRecords(List<Contact> contacts, string textFile)
    {
        List<string> lines = [];

        foreach (Contact contact in contacts)
        {
            lines.Add();
        }
    }
}