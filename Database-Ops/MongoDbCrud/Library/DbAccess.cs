
using Library.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library;

public class DbAccess
{
	private MongoClient _client;
	private IMongoDatabase _context;

	public DbAccess(string connectionString)
	{
		// Create a new client and connect to the server
		_client = new MongoClient(connectionString);

		// This connect us to a Database on the MondoDB
		_context = _client.GetDatabase("Crudbank");
	}

	// Create Operations
		
	public void InsertSingleData(string table, Account data)
	{
		var collection = _context.GetCollection<Account>(table);

		collection.InsertOne(data);
	}

	public void InsertSingleData(string table, BsonDocument data)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		collection.InsertOne(data);
	}

	public void InsertManyData(string table, List<Account> data)
	{
		var collection = _context.GetCollection<Account>(table);

		collection.InsertMany(data);
	}

	public void InsertManyData(string table, BsonDocument[] data)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		collection.InsertMany(data);
	}


	// Read Operations
	/***
	 * Find returns an IEnumerable
	 * We can append LINQ methods to the Find call
	 */

	public Account FindSingleData(string table, string accountId)
	{
		var collection = _context.GetCollection<Account>(table);

		var result = collection
			.Find(ac => ac.AccountId == accountId)
			.FirstOrDefault();

		return result;
	}

	public List<Account> FindAllData(string table)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = collection
			.Find(_ => true)
			.ToList();

		return results;
	}

	public List<Account> FindDataWithLinqFilters(string table, string accountType)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = collection
			.Find(ac => ac.AccountType == accountType)
			.SortByDescending(ac => ac.Balance)
			.Skip(2)
			.Limit(3)
			.ToList();

		return results;
	}

	public BsonDocument FindDataWithBuilderFilters(string table, string id, decimal balance = 0)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Eq("_id", new ObjectId(id));   // find id equals to specified value

		//var filter = Builders<BsonDocument>
		//	.Filter
		//	.Gt("balance", 1000);   // find documents whose balance greater than specified value

		var results = collection
			.Find(filter)
			.FirstOrDefault();

		return results;
	}


	// Update Operations
	/***
	 * UpdateOne accepts a query filter and the update defincation Then, returns an UpdateResult Object which has few useful properties:
	 * UpdateMany accepts a query filter and the update defincation Then, returns an UpdateResult Object which has few useful properties:
	 * 
	 * .IsAcknowledged (Boolean) - Tells us Was the update successfull?
	 * .MatchedCount (long) - Tells us How many documents were found?
	 * .ModifiedCount (long) - Tells us How many documents were updated?
	 */

	public UpdateResult UpdateSingleData(string table, string accountId, decimal balance)
	{
		var collection = _context.GetCollection<Account>(table);

		var filter = Builders<Account>
			.Filter
			.Eq(ac => ac.AccountId, accountId);   // find AccountId equals to specified value

		var update = Builders<Account>
			.Update
			.Set(ac => ac.Balance, balance);	// Update balance equals to specified value

		var results = collection.UpdateOne(filter, update);
		return results;
	}

	public UpdateResult UpdateManyData(string table, string accountType, decimal IncrementBalance)
	{
		var collection = _context.GetCollection<Account>(table);

		var filter = Builders<Account>
			.Filter
			.Eq(ac => ac.AccountType, accountType);   // find AccountType equals to specified value

		var update = Builders<Account>
			.Update
			.Inc(ac => ac.Balance, IncrementBalance);    // Increment the balance Amount by the specified amount

		var results = collection.UpdateMany(filter, update);
		return results;
	}

	public UpdateResult UpdateManyBsonData(string table, int queryBalance, int IncrementBalance)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Lt("balance", queryBalance);   // find AccountType equals to specified value

		var update = Builders<BsonDocument>
			.Update
			.Inc("balance", IncrementBalance);    // Increment the balance Amount by the specified amount

		var results = collection.UpdateMany(filter, update);
		return results;
	}


	// Delete Operation
	/***
	 * DeleteOne Deletes a single document from a collection accepts a query filter
	 * DeleteMany Deletes Many document from a collection accepts a query filter
	 * DeleteOne and DeleteMany method both retuurns DeleteResult obj
	 * 
	 * If DeleteMany method is invoked with empty query filter,
	 * all documents in the collection will be deleted.
	 * 
	 * We use use LINQ or Builders class for the Query filter
	 * Best Case: LINQ for C# Obj and Builders class for BsonDocument
	 */
	public DeleteResult DeleteSingleData(string table, string accountId)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = collection.DeleteOne(ac => ac.AccountId == accountId);
		return results;
	}

	public DeleteResult DeleteSingleWithBsonDataAndBuilders(string table, string id)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Eq("_id", new ObjectId(id));

		var results = collection.DeleteOne(filter);
		return results;
	}

	public DeleteResult DeleteManyData(string table, decimal balance)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = collection.DeleteMany(ac => ac.Balance <= balance);
		return results;
	}

	public DeleteResult DeleteManyWithBsonDataAndBuilders(string table, string accountType)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Eq("account_type", accountType);

		var results = collection.DeleteMany(filter);
		return results;
	}


	#region Transaction Methods

	public void BalanceTransferTransaction(string fromId, string toId, string trnsId, int trnsAmount)
	{
		var accountsCollection = _context.GetCollection<Account>("account");
		var transferCollection = _context.GetCollection<Transfer>("transfer");

		using var session = _client.StartSession();

		session.WithTransaction((s, ct) =>
		{
			// obtain the amount the money will be coming from
			var fromAccountResult = accountsCollection.Find(ac => ac.AccountId == fromId).FirstOrDefault();
			// Get the balance and id of the account that the money will be coming from
			decimal fromAccBalance = fromAccountResult.Balance - trnsAmount;
			string fromAccId = fromAccountResult.AccountId;

			// obtain the amount the money will be going to
			var toAccountResult = accountsCollection.Find(ac => ac.AccountId == toId).FirstOrDefault();
			// Get the balance and id of the account that the money will be going to
			decimal toAccBalance = toAccountResult.Balance + trnsAmount;
			string toAccId = toAccountResult.AccountId;

			// Create the transfer record
			var transferDocument = new Transfer
			{
				TransferId = trnsId,
				FromAccount = fromAccId,
				TransferAmount = trnsAmount,
				ToAccount = toAccId
			};

			// Update the balance and transfer array for debited account
			var fromAccFilter = Builders<Account>.Filter.Eq("account_id", fromId);

			var fromAccUpdateBalance = Builders<Account>.Update.Set("balance", fromAccBalance);			
			accountsCollection.UpdateOne(fromAccFilter, fromAccUpdateBalance);

			var fromAccUpdateTransfers = Builders<Account>.Update.Push("transfers_completed", trnsId);
			accountsCollection.UpdateOne(fromAccFilter, fromAccUpdateTransfers);

			// Update the balance and transfer array for credited account
			var toAccFilter = Builders<Account>.Filter.Eq("account_id", toId);

			var toAccUpdateBalance = Builders<Account>.Update.Set("balance", toAccBalance);
			accountsCollection.UpdateOne(toAccFilter, toAccUpdateBalance);

			var toAccUpdateTransfers = Builders<Account>.Update.Push("transfers_completed", trnsId);
			accountsCollection.UpdateOne(toAccFilter, toAccUpdateTransfers);

			// Insert transfer document
			transferCollection.InsertOne(transferDocument);

			Console.WriteLine("Transaction Complete.");
			return "Inserted into collections in different databases.";
		});
	}

	#endregion


	#region Async Methods

	// Create Operations

	public async Task InsertSingleDataAsync(string table, Account data)
	{
		var collection = _context.GetCollection<Account>(table);

		await collection.InsertOneAsync(data);
	}

	public async Task InsertSingleDataAsync(string table, BsonDocument data)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		await collection.InsertOneAsync(data);
	}

	public async Task InsertManyDataAsync(string table, List<Account> data)
	{
		var collection = _context.GetCollection<Account>(table);

		await collection.InsertManyAsync(data);
	}

	public async Task InsertManyDataAsync(string table, BsonDocument[] data)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		await collection.InsertManyAsync(data);
	}


	// Read Operations

	public async Task<Account> FindSingleDataAsync(string table, string accountId)
	{
		var collection = _context.GetCollection<Account>(table);

		var result = await collection
			.FindAsync(ac => ac.AccountId == accountId);

		return result.FirstOrDefault();
	}

	public async Task<List<Account>> FindAllDataAsync(string table)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = await collection
			.FindAsync(_ => true).Result.ToListAsync();

		return results;
	}

	public async Task<List<Account>> FindDataWithLinqFiltersAsync(string table, string accountType)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = await collection
			.Find(ac => ac.AccountType == accountType)
			.ToListAsync();

		results = results
			.OrderByDescending(ac => ac.Balance)
			.Skip(2)
			.Take(3)
			.ToList();

		return results;
	}

	public async Task<BsonDocument> FindDataWithBuilderFiltersAsync(string table, string id, decimal balance = 0)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		//var filter = Builders<BsonDocument>
		//	.Filter
		//	.Eq("_id", new ObjectId(id));   // find id equals to specified value

		var filter = Builders<BsonDocument>
			.Filter
			.Gt("balance", balance);   // find documents whose balance greater than specified value

		var results = await collection
			.FindAsync(filter);

		return results.FirstOrDefault();
	}


	// Update Operation

	public async Task<UpdateResult> UpdateSingleDataAsync(string table, string accountId, decimal balance)
	{
		var collection = _context.GetCollection<Account>(table);

		var filter = Builders<Account>
			.Filter
			.Eq(ac => ac.AccountId, accountId);   // find AccountId equals to specified value

		var update = Builders<Account>
			.Update
			.Set(ac => ac.Balance, balance);    // Update balance equals to specified value

		var results = await collection.UpdateOneAsync(filter, update);
		return results;
	}

	public async Task<UpdateResult> UpdateManyDataAsync(string table, string accountType, decimal IncrementBalance)
	{
		var collection = _context.GetCollection<Account>(table);

		var filter = Builders<Account>
			.Filter
			.Eq(ac => ac.AccountType, accountType);   // find AccountType equals to specified value

		var update = Builders<Account>
			.Update
			.Inc(ac => ac.Balance, IncrementBalance);    // Increment the balance Amount by the specified amount

		var results = await collection.UpdateManyAsync(filter, update);
		return results;
	}

	public async Task<UpdateResult> UpdateManyBsonDataAsync(string table, int queryBalance, int IncrementBalance)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Lt("balance", queryBalance);   // find AccountType equals to specified value

		var update = Builders<BsonDocument>
			.Update
			.Inc("balance", IncrementBalance);    // Increment the balance Amount by the specified amount

		var results = await collection.UpdateManyAsync(filter, update);
		return results;
	}


	// Delete Operation

	public async Task<DeleteResult> DeleteSingleDataAsync(string table, string accountId)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = await collection.DeleteOneAsync(ac => ac.AccountId == accountId);
		return results;
	}

	public async Task<DeleteResult> DeleteSingleWithBsonDataAndBuildersAsync(string table, string id)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Eq("_id", new ObjectId(id));

		var results = await collection.DeleteOneAsync(filter);
		return results;
	}

	public async Task<DeleteResult> DeleteManyDataAsync(string table, decimal balance)
	{
		var collection = _context.GetCollection<Account>(table);

		var results = await collection.DeleteManyAsync(ac => ac.Balance <= balance);
		return results;
	}

	public async Task<DeleteResult> DeleteManyWithBsonDataAndBuildersAsync(string table, string accountType)
	{
		var collection = _context.GetCollection<BsonDocument>(table);

		var filter = Builders<BsonDocument>
			.Filter
			.Eq("account_type", accountType);

		var results = await collection.DeleteManyAsync(filter);
		return results;
	}

	#endregion
}