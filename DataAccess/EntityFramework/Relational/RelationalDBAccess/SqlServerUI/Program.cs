using DataAccessLibrary.Models;
using DataAccessLibrary.SqlServer;
using Microsoft.Extensions.Configuration;

namespace SqlServerUI;

/***
 * Dapper With Command Timeout: https://code-maze.com/dotnet-handling-commandtimeout-with-dapper/
 * Repo: https://github.com/CodeMazeBlog/CodeMazeGuides/tree/main/dotnet-dapper/HandlingCommandTimeoutWithDapper
 */

sealed class Program
{
	static void Main()
	{
		SqlCRUD crud = new(GetConnectionString());

		ReadAllContacts(crud);
		//ReadContact(crud, 1004);
		//CreateNewContact(crud);
		//UpdateContact(crud);
		//RemovePhoneNumber(crud, 1004, 3);
		//ContactAdd(crud);


        Console.WriteLine(":: Done Processing Sql Server Database ::");
		Console.WriteLine(":: Learn From Tim Corey ::");
	}

	public static void CreateNewContact(SqlCRUD sql)
	{
		FullContact FC = new()
		{
			BasicInfo = new() { FirstName = "Debargo", LastName = "Bose" },
			EmailInfo = new() {
				new Email { EmailAddress = "kishan@rediff.com" },
				new Email { EmailAddress = "debargo@yahoo.com" }
			},
			PhoneInfo = new() {
				new Phone { PhoneNumber = "1549569785" },
				new Phone { PhoneNumber = "1597532584" }
			},
		};

		var returnId = sql.CreateContact(FC);
		ReadContact(sql, returnId);
	}

	private static void ReadContact(SqlCRUD sql, int contactId)
	{
		var contact = sql.GetFullContactsById(contactId);


		Console.WriteLine("::==========================================================::");
		Console.WriteLine($"Roll: {contact.BasicInfo.Id}, Full Name: {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");
		Console.WriteLine("Phone Numbers:");
		foreach (var item in contact.PhoneInfo)
		{
			Console.Write($"\tId: {item.Id}, Phone Number: {item.PhoneNumber}");
		}

		Console.WriteLine("\nEmails:");
		foreach (var item in contact.EmailInfo)
		{
			Console.Write($"\tId: {item.Id}, Email Address: {item.EmailAddress}");
		}
		Console.WriteLine("\n::==========================================================::");
	}

	private static void ReadAllContacts(SqlCRUD sql)
	{
		var dataObj = sql.GetAllContacts();

		foreach (var item in dataObj)
		{
			Console.WriteLine($"Roll: {item.Id}, Full Name: {item.FirstName} {item.LastName}");
		}
	}

	public static void UpdateContact(SqlCRUD sql)
	{
		BasicContact FC = new()
		{
			Id = 1005,
			FirstName = "Emily",
			LastName = "Sen"
		};

		var returnId = sql.UpdateContactName(FC);
		ReadContact(sql, returnId);
	}

    // Execute Stored Procedure and get the values as OUTPUT parameters
    public static void ContactAdd(SqlCRUD sql)
	{
		string firstName = "Koushik";
        string lastName = "Saha";

        var result = sql.ContactAdd(firstName, lastName);
		Console.WriteLine(result);
    }

	public static void RemovePhoneNumber(SqlCRUD sql, int contactId, int phoneId)
	{
		sql.RemovePhoneNumber(contactId, phoneId);
		ReadContact(sql, contactId);
	}

	private static string GetConnectionString(string ConnectionStringName = "Default")
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appSettings.json");

		var config = builder.Build();

		string output = config.GetConnectionString(ConnectionStringName) ?? string.Empty;
		return output;
	}
}