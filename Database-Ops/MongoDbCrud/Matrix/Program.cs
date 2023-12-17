
using Library;
using Library.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Matrix;

class Program
{
	private static readonly string tableName = "account";
	private static DbAccess db;

	static void Main()
	{
		db = new DbAccess(GetConnectionStringFromSecrets());

		// InsertData();	// Insert Single C# Object Document
		// InsertBsonData();	// Insert Single Bson Document
		// InsertManyData();   // Insert Many C# Object Document at once
		// InsertmanyBsonData();   // Insert Many Bson Document


		// FindSingleData();	// Get Single document
		// FindAll();	// Get All Documents
		// FindAllWithLinqFilters();	// Get filtered documents using Linq
		// FindAllWithBuilderFilters();	// Get filtered document using Builder class


		// UpdateSingleData();	// Update Single Document using Builder class and C# Obj
		// UpdateManyData();	// Update Many Document using Builder class and C# Obj
		// UpdateManyBsonData();   // Update Many Document using Builder class and Bson Document


		// DeleteSingleData();		// Delete Single Document using C# Obj and LINQ syntax
		// DeleteSingleBsonDataAndBuilders();	// Delete Single Document using BsonDocument with Builders Class
		// DeleteManyData();   // Delete Many Document using C# Obj and LINQ syntax
		// DeleteManyBsonDataAndBuilders();    // Delete Many Document using BsonDocument with Builders Class


		BalanceTransfer();	// Transfer Balance with MongoDB Transaction
	}

	public static void InsertData()
	{
		var data = new Account
		{
			AccountId = "CC266878963569",
			AccountHolder = "Travis",
			AccountType = "Checking",
			Balance = 5268789
		};

		db.InsertSingleData(tableName, data);
	}

	public static void InsertBsonData()
	{
		var data = new BsonDocument
		{
			{ "account_id", "BS159887496" },
			{ "account_holder", "Koushik" },
			{ "account_type", "Savings" },
			{ "balance", 15987499 },
		};

		db.InsertSingleData(tableName, data);
	}

	public static void InsertManyData()
	{
		var accountA = new Account
		{
			AccountId = "AAA266878963569",
			AccountHolder = "Shelby",
			AccountType = "Savings",
			Balance = 11000000
		};

		var accountB = new Account
		{
			AccountId = "ABB266878963569",
			AccountHolder = "Tarun",
			AccountType = "Current",
			Balance = 55000000
		};

		db.InsertManyData(tableName, new List<Account> { accountA, accountB });
	}

	public static void InsertmanyBsonData()
	{
		var data = new[]
		{
			new BsonDocument
			{
				{ "account_id", "BS159887496" },
				{ "account_holder", "Kartik" },
				{ "account_type", "Savings" },
				{ "balance", 15987499 },
			},
			new BsonDocument
			{
				{ "account_id", "BS159887496" },
				{ "account_holder", "Koushik" },
				{ "account_type", "Savings" },
				{ "balance", 15987499 },
			},
		};	

		db.InsertManyData(tableName, data);
	}


	public static void FindSingleData()
	{
		string accountId = "CC266878963569";

		var accData = db.FindSingleData(tableName, accountId);
		DisplayData(accData);	// Show result Data
	}

	public static void FindAll()
	{
		var accData = db.FindAllData(tableName);

        foreach (var item in accData)
        {
			DisplayData(item);   // Show result Data
		}
    }

	public static void FindAllWithLinqFilters()
	{
		string accountType = "Savings";
		var accData = db.FindDataWithLinqFilters(tableName, accountType);

		foreach (var item in accData)
		{
			DisplayData(item);   // Show result Data
		}
	}

	public static void FindAllWithBuilderFilters()
	{
		string id = "653e1abf7b023b448e6ee550";
		decimal balance = 1000;
		var accData = db.FindDataWithBuilderFilters(tableName, id, balance);

		Console.WriteLine(accData);
	}


	public static void UpdateSingleData()
	{
		string accountId = "BS159887496";
		decimal balance = 40000;
		var result = db.UpdateSingleData(tableName, accountId, balance);

		DisplayUpdateQueryStatus(result);
	}

	public static void UpdateManyData()
	{
		string accountType = "Current";
		decimal balance = 700;
		var result = db.UpdateManyData(tableName, accountType, balance);

		DisplayUpdateQueryStatus(result);
	}

	public static void UpdateManyBsonData()
	{
		int quybalance = 20000;
		int Incbalance = 50;
		var result = db.UpdateManyBsonData(tableName, quybalance, Incbalance);

		DisplayUpdateQueryStatus(result);
	}


	public static void DeleteSingleData()
	{
		string accountId = "CC266878963569";
		var status = db.DeleteSingleData(tableName, accountId);

		DisplayDeleteQueryStatus(status);
	}

	public static void DeleteSingleBsonDataAndBuilders()
	{
		string id = "653df74f63707bf28527150f";
		var status = db.DeleteSingleWithBsonDataAndBuilders(tableName, id);

		DisplayDeleteQueryStatus(status);
	}

	public static void DeleteManyData()
	{
		decimal balance = 15987499;
		var status = db.DeleteManyData(tableName, balance);

		DisplayDeleteQueryStatus(status);
	}

	public static void DeleteManyBsonDataAndBuilders()
	{
		string accountType = "Current";
		var status = db.DeleteManyWithBsonDataAndBuilders(tableName, accountType);

		DisplayDeleteQueryStatus(status);
	}

	
	public static void BalanceTransfer()
	{
		string fromId = "AAA266878963569";
		string toId = "ABB266878963569";

		// create a transfer_id and amount being transferred
		string transferId = "TR-sh-TR-000002";
		int transferAmount = 50000;

		db.BalanceTransferTransaction(fromId, toId, transferId, transferAmount);
	}


	// Update Operation Execution Status
	public static void DisplayUpdateQueryStatus(UpdateResult result)
	{
		Console.WriteLine($"Update Status {result.IsAcknowledged}");
		Console.WriteLine($"Total Matched Found {result.MatchedCount}");
		Console.WriteLine($"Total Document Update {result.ModifiedCount}");
	}

	// Delete Operation Execution Status
	public static void DisplayDeleteQueryStatus(DeleteResult result)
	{
		Console.WriteLine($"Delete Status {result.IsAcknowledged}");
		Console.WriteLine($"Total Deleted Count {result.DeletedCount}");
	}

	// Display Account Data
	private static void DisplayData(Account data)
	{
		Console.WriteLine($"Id: {data.Id}");
		Console.WriteLine($"\b\bAccount Id: {data.AccountId}");
		Console.WriteLine($"\b\bAccount Holder: {data.AccountHolder}");
		Console.WriteLine($"\b\bAccount Type: {data.AccountType}");
		Console.WriteLine($"\b\bAccount Balance: {data.Balance}");
	}

	// From User-Secrets get the Connection String
	private static string GetConnectionStringFromSecrets()
	{
		var config = new ConfigurationBuilder() // Needs Microsoft.Extensions.Configuration
				.AddUserSecrets<Program>()  // needs Microsoft.Extensions.UserSecrets
				.Build();

		string output = config["ConnectionStrings:Default"] ?? string.Empty;
		return output;
	}
}