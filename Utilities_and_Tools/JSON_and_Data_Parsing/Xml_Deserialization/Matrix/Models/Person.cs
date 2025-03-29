using System.Xml.Serialization;

namespace Matrix.Models;

/***
 * [XmlRoot("Person")]
 * Attribute specifies that the xml element representing an instance of the
 * Person type is named "Person".
 * 
 * 
 * [XmlElement("Name")]
 * Attribute map the Name and Age properties to the corresponding Xmlelements
 * within the "Person" element.
 */

[XmlRoot("Person")]
public class Person
{
    [XmlElement("Name")]
    public string Name { get; set; } = string.Empty;

	[XmlElement("Age")]
	public int Age { get; set; }
}