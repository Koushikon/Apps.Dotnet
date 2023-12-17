using System.Xml.Serialization;

namespace Matrix.Models;

[XmlRoot("Book")]
public class Book
{
	[XmlElement("Title")]
	public string Title { get; set; }

	[XmlElement("Author")]
	public string Author { get; set; }
}