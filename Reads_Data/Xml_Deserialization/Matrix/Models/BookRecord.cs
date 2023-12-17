using System.Xml.Serialization;

namespace Matrix.Models;

public record BookRecord([property: XmlElement("Title")] string Title, [property: XmlElement("Author")] string Author)
{
	public BookRecord() : this("", "") { }
}