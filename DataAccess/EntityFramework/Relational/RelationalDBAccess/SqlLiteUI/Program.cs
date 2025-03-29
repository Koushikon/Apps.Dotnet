using DataAccessLibrary.Models;
using DataAccessLibrary.Sqlite;
using Microsoft.Extensions.Configuration;

namespace SqLiteUI;

sealed class Program
{
	static void Main()
	{
		SqLiteCrud crud = new(GetConnectionString());

		ReadAllContacts(crud);
		//ReadContact(crud, 10);
		//CreateNewContact(crud);
		//UpdateContact(crud);
		//RemovePhoneNumber(crud, 10, 5);

		Console.WriteLine(":: Done Processing SqlLite Database ::");
		Console.WriteLine(":: Learn From Tim Corey ::");
	}

	public static void CreateNewContact(SqLiteCrud sql)
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

	private static void ReadContact(SqLiteCrud sql, int contactId)
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

	private static void ReadAllContacts(SqLiteCrud sql)
	{
		var dataObj = sql.GetAllContacts();

		foreach (var item in dataObj)
		{
			Console.WriteLine($"Roll: {item.Id}, Full Name: {item.FirstName} {item.LastName}");
		}
	}

	public static void UpdateContact(SqLiteCrud sql)
	{
		BasicContact FC = new()
		{
			Id = 10,
			FirstName = "Emily",
			LastName = "Sen"
		};

		var returnId = sql.UpdateContactName(FC);
		ReadContact(sql, returnId);
	}

	public static void RemovePhoneNumber(SqLiteCrud sql, int contactId, int phoneId)
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
