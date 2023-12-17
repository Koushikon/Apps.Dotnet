using System.Xml.Serialization;

namespace Matrix.Models;

/***
 * Handling Complex Types and Relationships
 * 
 * [XmlArray("Books")]
 * Attribute specifies the name of the xml element containing the list of Book.
 * 
 * [XmlArrayItem("Book")]
 * Attribute specifies that each item within the "Books" element
 * should have a representation as an Xml element named "Book".
 */

[XmlRoot("Library")]
public class Library
{
	[XmlArray("Books")]
	[XmlArrayItem("Book")]
	public List<Book> Books { get; set; } = new List<Book>();
}