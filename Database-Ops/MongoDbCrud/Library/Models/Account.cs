using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models;

public class Account
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; set; } = String.Empty;

	[BsonElement("account_id")]
	public string AccountId { get; set; } = String.Empty;

	[BsonElement("account_holder")]
	public string AccountHolder { get; set; } = String.Empty;

	[BsonElement("account_type")]
	public string AccountType { get; set; } = String.Empty;

	[BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
	[BsonElement("balance")]
	public decimal Balance { get; set; }

	[BsonElement("transfers_completed")]
	public string[] TransfersCompleted { get; set; } = Array.Empty<string>();
}