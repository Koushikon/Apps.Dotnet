using Matrix.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Matrix;

public class JsonComparision
{
	public string PlainJsonString { get; set; } = string.Empty;
	public string SecondPlainJsonString { get; set; } = string.Empty;
	public string NestedJsonString { get; set; } = string.Empty;

	public JsonComparision()
	{
		InitializeData();
	}


	public void InitializeData()
	{
		var testData = new TestData();
		PlainJsonString = testData.GeneratePlainJsonString();
		SecondPlainJsonString = testData.GeneratePlainJsonString();
		NestedJsonString = testData.GenerateNestedJsonString();
	}

	/***
	 * JToken.DeepEquals()
     *  We use it when comparing two JSON objects,
     *  Including the deeply nested objects.
     *  This returns bool
     */
	public Dictionary<string, string> CompareJsonObjectsUsingDeepEquals()
	{
		var results = new Dictionary<string, string>();
		var plainJsonObject = JToken.Parse(PlainJsonString);
		var secondJsonObject = JToken.Parse(SecondPlainJsonString);
		var nestedJsonObject = JToken.Parse(NestedJsonString);

		var arePlainObjectsEqual = JToken.DeepEquals(plainJsonObject, secondJsonObject);
		var isPlainAndNestedObjectsEqual = JToken.DeepEquals(plainJsonObject, nestedJsonObject);

		string responseString;

		if (arePlainObjectsEqual)
		{
			responseString = "The Plain Json Objects are equal.";
			results.Add("Plain Objects Result", responseString);
		}
		else
		{
			responseString = "The Plain Json Objects are not equal.";
			results.Add("Plain Objects Result", responseString);
		}

		if (isPlainAndNestedObjectsEqual)
		{
			responseString = "The Plain and Nested Json Objects are equal.";
			results.Add("Nested Objects Result", responseString);
		}
		else
		{
			responseString = "The Plain and Nested Json Objects are not equal.";
			results.Add("Nested Objects Result", responseString);
		}

		return results;
	}


	/***
	 * Equals()
	 * Deserializing the JSON strings produces objects with this same schema.
	 * After deserializing the objects, we use the Equals() method to compare the objects. 
	 */
	public Dictionary<string, string> CompareDeserializedJsonObjects()
	{
		var results = new Dictionary<string, string>();
		var car1 = JsonConvert.DeserializeObject<Car>(PlainJsonString);
		var car2 = JsonConvert.DeserializeObject<Car>(SecondPlainJsonString);
		var car3 = JsonConvert.DeserializeObject<Car>(NestedJsonString);

		string responseString;

		if (car1 != null && car1.Equals(car2))
		{
			responseString = "The two deserialized Plain Json Objects are equal.";
			results.Add("Plain Objects Result", responseString);
		}
		else
		{
			responseString = "The two deserialized Plain Json Objects are not equal.";
			results.Add("Plain Objects Result", responseString);
		}

		if (car1 != null && car1.Equals(car3))
		{
			responseString = "The Plain and Nested deserialized Json Objects are equal.";
			results.Add("Nested Objects Result", responseString);
		}
		else
		{
			responseString = "The Plain and Nested deserialized Json Objects are not equal.";
			results.Add("Nested Objects Result", responseString);
		}

		return results;
	}


	/***
	 *  we first convert the JSON strings into JObject objects using JObject.Parse.
	 *  Then, we compare the two plain objects using LINQ by iterating through all the properties,
	 *  checking whether the values of the properties match each property. If the values of the JSON properties are equal,
	 *  the method returns true, otherwise, it returns false.
	 */
	public Dictionary<string, string> CompareJsonObjectsUsingLinq()
	{
		var results = new Dictionary<string, string>();
		var plainJsonObject = JObject.Parse(PlainJsonString);
		var secondJsonObject = JObject.Parse(SecondPlainJsonString);
		var nestedJsonObject = JObject.Parse(NestedJsonString);

		var arePlainObjectsEqual = plainJsonObject.Properties().All(p => p.Value.Equals(secondJsonObject[p.Name]));
		var isPlainAndNestedObjectsEqual = nestedJsonObject.Properties().All(p => p.Value.Equals(plainJsonObject[p.Name]));

		string responseString;

		if (arePlainObjectsEqual)
		{
			responseString = "Using LINQ The Plain Json Objects are equal.";
			results.Add("Plain Objects Result", responseString);
		}
		else
		{
			responseString = "Using LINQ The Plain Json Objects are not equal.";
			results.Add("Plain Objects Result", responseString);
		}

		if (isPlainAndNestedObjectsEqual)
		{
			responseString = "Using LINQ The Plain and Nested Json Objects are equal.";
			results.Add("Nested Objects Result", responseString);
		}
		else
		{
			responseString = "Using LINQ The Plain and Nested Json Objects are not equal.";
			results.Add("Nested Objects Result", responseString);
		}

		return results;
	}
}