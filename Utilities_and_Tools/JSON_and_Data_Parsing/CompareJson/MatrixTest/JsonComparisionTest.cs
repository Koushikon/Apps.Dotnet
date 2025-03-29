using Matrix;

namespace MatrixTest;

public class JsonComparisionTest
{
	private readonly JsonComparision _jsonComparision;

	public JsonComparisionTest()
	{
		_jsonComparision = new JsonComparision();
	}

	[Theory]
	[InlineData("Plain Objects Result", "The Plain Json Objects are equal.")]
	[InlineData("Nested Objects Result", "The Plain and Nested Json Objects are not equal.")]
	public void WhenJTokenDeepEqualsUsed_ThenCompareObjectsAndPopulateDictionary(string key, string expectedValue)
	{
		var result = _jsonComparision.CompareJsonObjectsUsingDeepEquals();

		Assert.Equal(expectedValue, result[key]);
	}


	[Theory]
	[InlineData("Plain Objects Result", "The two deserialized Plain Json Objects are equal.")]
	[InlineData("Nested Objects Result", "The Plain and Nested deserialized Json Objects are not equal.")]
	public void WhenDeserializedObjectsUsed_ThenCompareObjectsAndPopulateDictionary(string key, string expectedValue)
	{
		var result = _jsonComparision.CompareDeserializedJsonObjects();

		Assert.Equal(expectedValue, result[key]);
	}


	[Theory]
	[InlineData("Plain Objects Result", "Using LINQ The Plain Json Objects are equal.")]
	[InlineData("Nested Objects Result", "Using LINQ The Plain and Nested Json Objects are not equal.")]
	public void WhenUsingLinqComparisionUsed_ThenCompareObjectsAndPopulateDictionary(string key, string expectedValue)
	{
		var result = _jsonComparision.CompareJsonObjectsUsingLinq();

		Assert.Equal(expectedValue, result[key]);
	}
}