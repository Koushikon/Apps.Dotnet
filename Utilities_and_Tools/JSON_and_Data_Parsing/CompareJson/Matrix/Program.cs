namespace Matrix;

// Learn Source: https://code-maze.com/csharp-compare-two-json-objects/

internal class Program
{
	static void Main(string[] args)
	{
		JsonComparision jsonCompare = new();

		var result1 = jsonCompare.CompareJsonObjectsUsingDeepEquals();

		DisplayResult(result1);

		var result2 = jsonCompare.CompareDeserializedJsonObjects();

		DisplayResult(result2);

		var result3 = jsonCompare.CompareJsonObjectsUsingLinq();

		DisplayResult(result3);

		Console.WriteLine("Good Night, World!!");
	}

	public static void DisplayResult(Dictionary<string, string> dataItems)
	{
		foreach (var item in dataItems)
		{
			Console.WriteLine(item.Value);
		}
	}
}