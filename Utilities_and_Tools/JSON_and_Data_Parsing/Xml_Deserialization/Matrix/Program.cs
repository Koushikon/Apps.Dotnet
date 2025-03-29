using Matrix.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Matrix;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("|| = = = Hello, World!");

				
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\Files\");

		var file = "Person.xml";    // Person xml file name
		ReadPersonXml(filePath + file);	// Deserialize Library Xml data


		file = "Library.xml";   // Library xml file name
		ReadLibraryXml(filePath + file);	// Deserialize Complex Library Xml data


		XmlDeSerializerException();	// Xml Deserialization exception info


		var xmlData = """
						<PersonRecord>
							<Name>Bakshi Babu</Name>
							<Age>27</Age>
						</PersonRecord>
						""";

		var personRecord1 = DeserializeXmlData<PersonRecord>(xmlData);  // Deserialize PersonRecord Xml data into a record type
		if (personRecord1 != null)
		{
			Console.WriteLine($"Name: {personRecord1.Name} and Age: {personRecord1.Age}\n");
		}		


		var xmlData2 = """
                            <Person>
                                <Name>John Doe</Name>
                                <Age>30</Age>
                            </Person>
                        """;
		var person1 = DeserializeXmlData<Person>(xmlData2);  // Deserialize Person Xml data into a record type
		if (person1 != null)
		{
			Console.WriteLine($"Name: {person1.Name} and Age: {person1.Age}\n");
		}


		var libraryXml = """
							<LibraryRecord>
								<Books>
									<BookRecord>
										<Title>Title 3</Title>
										<Author>Author 3</Author>
									</BookRecord>
									<BookRecord>
										<Title>Title 4</Title>
										<Author>Author 4</Author>
									</BookRecord>
								</Books>
							</LibraryRecord>
						""";
		var libraryRecord1 = DeserializeXmlData<LibraryRecord>(libraryXml); // Deserialize Complex Library Xml data into a record type
		if (libraryRecord1 != null)
		{
			foreach (var item in libraryRecord1.Books)
			{
				Console.WriteLine($"Title: {item.Title} and Author: {item.Author}\n");
			}
		}


		Console.WriteLine("|| = = = Goog Night, World!");
	}

	static T? DeserializeXmlData<T>(string filePath)
	{
		var serializer = new XmlSerializer(typeof(T));

		using StringReader reader = new StringReader(filePath);

		var result = (T)serializer.Deserialize(reader)!;
		return result;
	}

	static void ReadPersonXml(string path)
	{
		var serializer = new XmlSerializer(typeof(Person));

		using (var reader = new StreamReader(path))
		{
			var person = (Person)serializer.Deserialize(reader)!;
			Console.WriteLine($"Name: {person.Name} and Age: {person.Age}\n");
		}
	}

	static void ReadLibraryXml(string path)
	{
		var serializer = new XmlSerializer(typeof(Library));

		using (var reader = new StreamReader(path))
		{
			var library = (Library)serializer.Deserialize(reader)!;

			foreach (var item in library.Books)
			{
				Console.WriteLine($"Title: {item.Title} and Author: {item.Author}\n");
			}
		}
	}


	/***
	 * Error Handling and Exception in XML Deserialization
	 * 
	 * InvalidOperationException: indicates that the Xml data does not conform to the expected
	 * format or defined targated structure.
	 * 
	 * XmlException: occurrs when data is invalid contains syntax error.
	 * 
	 * NotSupportedException: occur when deserialization process or the chosen Xml serializer
	 * does not support a specific Xml feature or construct.
	 */

	static void XmlDeSerializerException()
	{
		try
		{
			// Xml deserialization code
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
		catch (XmlException ex)
		{
			Console.WriteLine($"Xml Error at line {ex.LineNumber}: {ex.Message}");
		}
		catch (NotSupportedException ex)
		{
			Console.WriteLine($"Supported operation: {ex.Message}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}
}