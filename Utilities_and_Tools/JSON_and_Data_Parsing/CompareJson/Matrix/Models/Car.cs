namespace Matrix.Models;

public class Car
{
    public string Name { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
	public string Model { get; set; } = string.Empty;


	/***
	 * If we Initialize this call with new it will not match
	 * Because the Equals code condition is wrote that way it check the whole object not the individual properties of that object
	 * Or, we can change the condition yo check those properties and that will work.
	 */
	public Price? Price { get; set; }

	public override bool Equals(object? obj)
	{
		if(obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		var car = (Car)obj;

		return Name == car.Name && Make == car.Make && Model == car.Model && Price == car.Price;
	}
}