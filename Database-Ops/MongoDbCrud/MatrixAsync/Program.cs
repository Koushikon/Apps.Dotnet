using Library;
using Library.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MatrixAsync;

public class Program
{
	private static readonly string tableName = "account";
	private static DbAccess db;

	static async Task Main(string[] args)
	{
		db = new DbAccess(GetConnectionStringFromSecrets());

		// await InsertDataAsync();	// Insert Single C# Object Document asynchronously
		// await InsertBsonDataAsync();	// Insert Single Bson Document asynchronously
		// await InsertManyDataAsync();  // Insert Many C# Object Document at once asynchronously
		// await InsertmanyBsonDataAsync();  // Insert Many Bson Document asynchronously


		// await FindSingleData();	// Get Single document
		// await FindAllAsync();	// Get All Documents
		// await FindAllWithLinqFiltersAsync();   // Get filtered documents using Linq
		// await FindAllWithBuilderFiltersAsync();    // Get filtered document using Builder class


		// await UpdateSingleDataAsync();	// Update Single Document using Builder class and C# Obj
		// await UpdateManyDataAsync();		// Update Many Document using Builder class and C# Obj
		// await UpdateManyBsonDataAsync();	// Update Many Document using Builder class and Bson Document


		// await DeleteSingleDataAsync();     // Delete Single Document using C# Obj and LINQ syntax
		// await DeleteSingleBsonDataAndBuildersAsync();  // Delete Single Document using BsonDocument with Builders Class
		// await DeleteManyDataAsync();   // Delete Many Document using C# Obj and LINQ syntax
		// await DeleteManyBsonDataAndBuildersAsync();    // Delete Many Document using BsonDocument with Builders Class

		Console.WriteLine("Good Bye, World!");
	}

	private async static Task InsertDataAsync()
	{
		var data = new Account
		{
			AccountId = "GC159856598",
			AccountHolder = "Anirban",
			AccountType = "Current",
			Balance = 23698574
		};

		await db.InsertSingleDataAsync(tableName, data);
	}

	private async static Task InsertBsonDataAsync()
	{
		var data = new BsonDocument
		{
			{ "account_id", "SV159847596" },
			{ "account_holder", "Sourav" },
			{ "account_type", "Savings" },
			{ "balance", 445566997 },
		};

		await db.InsertSingleDataAsync(tableName, data);
	}

	private async static Task InsertManyDataAsync()
	{
		var accountA = new Account
		{
			AccountId = "BYM12459853677",
			AccountHolder = "Byomkesh",
			AccountType = "Current",
			Balance = 100000000
		};

		var accountB = new Account
		{
			AccountId = "SLK00145678926",
			AccountHolder = "Sherlock",
			AccountType = "Current",
			Balance = 100000000
		};

		await db.InsertManyDataAsync(tableName, new List<Account> { accountA, accountB });
	}

	private async static Task InsertmanyBsonDataAsync()
	{
		var data = new[]
		{
			new BsonDocument
			{
				{ "account_id", "TOM126598756" },
				{ "account_holder", "Tom" },
				{ "account_type", "Savings" },
				{ "balance", 2200446677 },
			},
			new BsonDocument
			{
				{ "account_id", "HNR0369874569" },
				{ "account_holder", "Henry" },
				{ "account_type", "Savings" },
				{ "balance", 1122339977 },
			},
		};

		await db.InsertManyDataAsync(tableName, data);
	}


	public async static Task FindSingleData()
	{
		string accountId = "CC266878963569";

		var accData = await db.FindSingleDataAsync(tableName, accountId);
		DisplayData(accData);   // Show result Data
	}

	public async static Task FindAllAsync()
	{
		var accData = await db.FindAllDataAsync(tableName);

		foreach (var item in accData)
		{
			DisplayData(item);   // Show result Data
		}
	}

	public async static Task FindAllWithLinqFiltersAsync()
	{
		string accountType = "Savings";
		var accData = await db.FindDataWithLinqFiltersAsync(tableName, accountType);

		foreach (var item in accData)
		{
			DisplayData(item);   // Show result Data
		}
	}

	public async static Task FindAllWithBuilderFiltersAsync()
	{
		string id = "653e1abf7b023b448e6ee550";
		decimal balance = 15987499;
		var accData = await db.FindDataWithBuilderFiltersAsync(tableName, id, balance);

		Console.WriteLine(accData);
	}


	public async static Task UpdateSingleDataAsync()
	{
		string accountId = "BS159887496";
		decimal balance = 5000;
		var result = await db.UpdateSingleDataAsync(tableName, accountId, balance);

		DisplayUpdateQueryStatus(result);
	}

	public async static Task UpdateManyDataAsync()
	{
		string accountType = "Current";
		decimal balance = 700;
		var result = await db.UpdateManyDataAsync(tableName, accountType, balance);

		DisplayUpdateQueryStatus(result);
	}

	public async static Task UpdateManyBsonDataAsync()
	{
		int quybalance = 20000;
		int Incbalance = 50;
		var result = await db.UpdateManyBsonDataAsync(tableName, quybalance, Incbalance);

		DisplayUpdateQueryStatus(result);
	}


	public static async Task DeleteSingleDataAsync()
	{
		string accountId = "BS159887496";
		var status = await db.DeleteSingleDataAsync(tableName, accountId);

		DisplayDeleteQueryStatus(status);
	}

	public static async Task DeleteSingleBsonDataAndBuildersAsync()
	{
		string id = "653e1ab67b023b448e6ee54e";
		var status = await db.DeleteSingleWithBsonDataAndBuildersAsync(tableName, id);

		DisplayDeleteQueryStatus(status);
	}

	public async static Task DeleteManyDataAsync()
	{
		decimal balance = 445566997;
		var status = await db.DeleteManyDataAsync(tableName, balance);

		DisplayDeleteQueryStatus(status);
	}

	public async static Task DeleteManyBsonDataAndBuildersAsync()
	{
		string accountType = "Savings";
		var status = await db.DeleteManyWithBsonDataAndBuildersAsync(tableName, accountType);

		DisplayDeleteQueryStatus(status);
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