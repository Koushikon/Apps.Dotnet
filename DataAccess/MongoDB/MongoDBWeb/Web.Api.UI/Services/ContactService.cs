using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Web.Api.UI.Models;

namespace Web.Api.UI.Services;

public class ContactService
{
	private readonly IMongoCollection<ContactModel> _contactCollection;

	public ContactService(IOptions<DatabaseSetting> dbSettings)
	{
		// Create a new client and connect to the server
		var client = new MongoClient(dbSettings.Value.ConnectionString);

		// This connect us to a Database on the MondoDB
		var mongoDb = client.GetDatabase(dbSettings.Value.DatabaseName);

		_contactCollection = mongoDb.GetCollection<ContactModel>(dbSettings.Value.CollectionName);
	}

	public async Task<List<ContactModel>> GetAllContacts() =>
		await _contactCollection.Find(_ => true).ToListAsync();

	public async Task<ContactModel?> GetContactById(string id) =>
	  await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

	public async Task<List<ContactModel>> GetContactByFirstName(string firstName) =>
	  await _contactCollection.Find(x => x.FirstName == firstName).ToListAsync();

	public async Task CreateContact(ContactModel newContact) =>
	  await _contactCollection.InsertOneAsync(newContact);

	public async Task UpdateContact(string id, ContactModel updatedContact) =>
	  await _contactCollection.ReplaceOneAsync(x => x.Id == id, updatedContact);

	public async Task RemoveContact(string id) =>
	  await _contactCollection.DeleteOneAsync(x => x.Id == id);
}