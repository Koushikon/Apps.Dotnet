using Library;
using Library.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DatabaseUI;

class Program
{
	private static readonly string tableName = "Contacts";
	private static MongoDBDataAccess db;

	static void Main()
	{
		// From appSettings.json
		//db = new MongoDBDataAccess("NoSQLCrud", GetConnectionString());

		// From User-Secrets
		db = new MongoDBDataAccess("NoSQLCrud", GetConnectionStringFromSecrets());

		// Set the data
		var contact = SetContactData();

		//CreateContact(contact);	// Create new contacts		

		//GetContactById("f9c8d469-93fc-4428-8989-daeddf7b2ab4");	// Get contact data by id

		//UpdateContactName("Jimmy", "Falcon", "87920f53-acce-4e0b-a312-993b1aa140a0"); // Update the Contact name of the document

		GetAllContacts();   // Get all contacts

		//RemovePhoneNumber("456-666-4875", "299f2295-c4cd-49a4-b4dd-4cec46f92e64");	// Remove the Contact Phone Number of the document

		//RemoveUser("87920f53-acce-4e0b-a312-993b1aa140a0");	// Remove the single contact document

		Console.WriteLine("Done processing MondoDB.");
	}

	private static void CreateContact(ContactModel contact)
	{
		//db.UpsertRecord(tableName, contact.Id, contact);

		db.InsertRecord(tableName, contact);
	}

	private static void UpdateContactName(string firstName, string lastName, string id)
	{
		Guid guid = new Guid(id);
		var contact = db.LoadRecordById<ContactModel>(tableName, guid);

		contact.FirstName = firstName;
		contact.LastName = lastName;

		db.UpsertRecord<ContactModel>(tableName, contact.Id, contact);
	}

	private static void GetAllContacts()
	{
		var contacts = db.LoadRecords<ContactModel>(tableName);

		foreach (var item in contacts)
		{
			DisplayContact(item);
		}
	}

	private static void RemoveUser(string id)
	{
		Guid guid = new Guid(id);

		db.DeleteRecord<ContactModel>(tableName, guid);
	}

	private static void RemovePhoneNumber(string phoneNumber, string id)
	{
		Guid guid = new Guid(id);
		var contact = db.LoadRecordById<ContactModel>(tableName, guid);

		contact.PhoneNumbers = contact.PhoneNumbers.Where(x => x.PhoneNumber != phoneNumber).ToList();

		db.UpsertRecord<ContactModel>(tableName, contact.Id, contact);
	}

	private static void GetContactById(string id)
	{
		Guid guid = new Guid(id);
		var contact = db.LoadRecordById<ContactModel>(tableName, guid);

		DisplayContact(contact);
	}


	private static ContactModel SetContactData()
	{
		ContactModel contact = new ContactModel
		{
			FirstName = "Shelly",
			LastName = "Dan",
			EmailIds = new List<EmailModel>
			{
				new EmailModel { EmailId = "Shelly@arc.com" },
				new EmailModel { EmailId = "Shelly_111@ten.com" }
			},
			PhoneNumbers = new List<PhoneModel>
			{
				new PhoneModel { PhoneNumber = "456-666-4875" },
				new PhoneModel { PhoneNumber = "789-555-5566" }
			}
		};

		return contact;
	}

	private static void DisplayContact(ContactModel contact)
	{
		Console.WriteLine($"_id: {contact.Id}, First Name: {contact.FirstName}, LastName: {contact.LastName}");

		foreach (var item in contact.EmailIds)
		{
			Console.WriteLine($"  Email Address: {item.EmailId}");
		}
		foreach (var item in contact.PhoneNumbers)
		{
			Console.WriteLine($"  Mobile Number: {item.PhoneNumber}");
		}
	}

	// From appSettings.json get the Connection String
	private static string GetConnectionString(string ConnectionStringName = "Default")
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())   // Needs Microsoft.Extensions.Configuration.Json
			.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

		var config = builder.Build();

		string output = config.GetConnectionString(ConnectionStringName) ?? string.Empty;
		return output;
	}

	// From User-Secrets get the Connection String
	private static string GetConnectionStringFromSecrets()
	{
		var config = new ConfigurationBuilder() // Needs Microsoft.Extensions.Configuration
				.AddUserSecrets<Program>()	// needs Microsoft.Extensions.UserSecrets
				.Build();

		string output = config["ConnectionStrings:Default"] ?? string.Empty;
		return output;
	}
}