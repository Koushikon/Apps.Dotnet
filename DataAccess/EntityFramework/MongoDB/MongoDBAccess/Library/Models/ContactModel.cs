using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models;

public class ContactModel
{
	/***
     * [BsonRepresentation(BsonType.String)]
     * WIth this attribute MongoDB saves the _id as guid form Otherwise,
     * GUIDs in MongoDB were represented as BsonBinaryData values of subtype 3.
     */
	[BsonId]
	//[BsonRepresentation(BsonType.String)]
	public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;

    public List<EmailModel> EmailIds { get; set; } = new List<EmailModel>();
    public List<PhoneModel> PhoneNumbers { get; set; } = new List<PhoneModel>();
}
