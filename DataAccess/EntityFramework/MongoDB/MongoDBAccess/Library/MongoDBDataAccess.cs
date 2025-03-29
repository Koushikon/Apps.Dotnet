using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Library;

public class MongoDBDataAccess
{
    private IMongoDatabase _context;

    public MongoDBDataAccess(string dbName, string connectionString)
    {
		// Create a new client and connect to the server
		var client = new MongoClient(connectionString);

        TestDBConnection(client);
        GetAllDatabaseNames(client);

		// This connect us to a Database on the MondoDB
		_context = client.GetDatabase(dbName);
	}


    public void TestDBConnection(MongoClient client)
    {
		// Test the Connection
		try
		{
			var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
			Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}


    public void GetAllDatabaseNames(MongoClient client)
    {
		// Get all the database from the cluster
		var dbList = client.ListDatabases().ToList();
		foreach (var db in dbList)
		{
			Console.WriteLine(db);
		}
	}


	public void InsertRecord<T>(string table, T record)
    {
        var collection = _context.GetCollection<T>(table);
		collection.InsertOne(record);
    }


    public List<T> LoadRecords<T>(string table)
    {
        var collection = _context.GetCollection<T>(table);
        return collection.Find(new BsonDocument()).ToList();
    }


    public T LoadRecordById<T>(string table, Guid id)
    {
        var collection = _context.GetCollection<T>(table);

		// Configure the filters for the search
		var filter = Builders<T>.Filter.Eq("Id", id);

        return collection.Find(filter).First();
    }


    public void UpsertRecord<T>(string table, Guid id, T record)
    {
		var collection = _context.GetCollection<T>(table);

        var result = collection.ReplaceOne(
            new BsonDocument("_id", id),
            record,
            new ReplaceOptions { IsUpsert = true });
	}


    public void DeleteRecord<T>(string table, Guid id)
    {
		var collection = _context.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("Id", id);
        collection.DeleteOne(filter);
	}
}