// With Newtonsoft.Json Package
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// With System.Text.Json Package
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Matrix;


/***
 * Source: https://code-maze.com/csharp-how-to-iterate-over-json-objects/
 * 
 * Json.NET :: comes with a lot of options by default:
 * Case-insensitivity
 * No depth limit
 * Allowing comments and trailing commas
 * Support for strings with single as well as double quotes
 * Support for non-string values
 * 
 * 
 * System.Text.Json :: on the other hand:
 * requires us to configure it so we can get desired results
 */


public class JsonIteration
{
    public string Json { get; set; }

    public JsonIteration()
    {
        //Json = new TestData().GenerateNullData()!;
        Json = new TestData().GenerateJsonData();

        if (string.IsNullOrWhiteSpace(Json))
        {
            throw new Exception("No Data was Found!");
        }
    }


    /***
     * Using Json.NET
     * Deserialize the json data into a dynamic Object
     */
    public int IterateOverJsonDynamically()
    {
        var jsonData = JsonConvert.DeserializeObject<dynamic>(Json)!;

        foreach (var item in jsonData)
        {
            var name = item.name;
            var age = item.age;
            var department = item.department;

            Console.WriteLine($"Name: {name}, Age: {age}, Department: {department}.");
        }
        Console.WriteLine();

        return jsonData.Count;
    }


    /***
     * Using Json.NET
     * Deserialize the json data into a JArray Object
     * Then, GEt the value by passsing the keys using square bracket notation
     */
    public int IterateUsingJArray()
    {
        var jsonArray = JArray.Parse(Json);

        foreach (var item in jsonArray)
        {
            var name = Convert.ToString(item["name"]);
            var age = Convert.ToInt32(item["age"]);
            var department = Convert.ToString(item["department"]);

            Console.WriteLine($"Name: {name}, Age: {age}, Department: {department}.");
        }
        Console.WriteLine();

        return jsonArray.Count;
    }


    /***
     * Using Json.NET
     * Deserialize the json data into a Statically Typed Object
     * Then, GEt the value by passsing the keys using square bracket notation
     */
    public int IterateUsingStaticallyTypeObject()
    {
        var resultData = JsonConvert.DeserializeObject<List<Employee>>(Json)!;

        foreach (var item in resultData)
        {
            Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Department: {item.Department}.");
        }
        Console.WriteLine();

        return 0;
    }


    /***
     * Using System.Text.Json
     * First Set up the JsonSerializerOptions, Then pass it with the data
     * Deserialize the json data into a Statically Typed Object
     */
    public int IterateUsingSystemJson()
    {
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var employees = JsonSerializer.Deserialize<List<Employee>>(Json, jsonOptions)!;

        foreach (var item in employees)
        {
            Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Department: {item.Department}.");
        }
        Console.WriteLine();

        return employees.Count;
    }
}