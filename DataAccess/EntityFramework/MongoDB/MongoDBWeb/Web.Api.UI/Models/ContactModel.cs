using MongoDB.Bson.Serialization.Attributes;

namespace Web.Api.UI.Models;

public class ContactModel
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string? Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;

	public List<EmailModel> EmailIds { get; set; } = new List<EmailModel>();
	public List<PhoneModel> PhoneNumbers { get; set; } = new List<PhoneModel>();
}
