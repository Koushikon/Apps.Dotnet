using BasicTest.BusinessLogic;
using System.Collections;
namespace BasicTest;

public class CalculatorTest
{
    // Basic Test with [Fact] attribute
    [Fact]
    public void CalculateAddWithFact_ReturnAddValue()
    {
        // Arrenge
        var calculator = new Calculator();
        int value1 = 5;
        int value2 = 6;

        // Act
        int result = calculator.Add(value1, value2);

        // Assert
        Assert.Equal(11, result);
    }



    /***
     * Parameterised Test with [Theory] with [InlineData]
     * Instead of specifying the values to add (value1 and value2) in the test body,
     * we pass those values as parameters to the test. We also pass in the expected result of the calculation,
     * to use in the Assert.Equal() call.
     * 
     * The data is provided by the [InlineData] attribute. Each instance of [InlineData] will create a
     * separate execution of the method. The values passed in the constructor of [InlineData]
     * are used as the parameters for the method - the order of the parameters in the attribute matches the
     * order in which they're supplied to the method.
     * 
     * Also we must follow the define method no. of parameterm, type etc When passing to the [InlineData] attribute
     */
    [Theory]
    [InlineData(5, 6, 11)]
    [InlineData(50, 56, 106)]
    [InlineData(51, 60, 111)]
    [InlineData(-84, 90, 6)]
    [InlineData(117, -33, 84)]
    [InlineData(150, -75, 75)]
    //[InlineData(150, -75)]    // Gets Error
    //[InlineData(150, -75, 75, 59)]    // Gets Error
    [InlineData(int.MinValue, -1, int.MaxValue)]
    [InlineData(int.MaxValue, 1, int.MinValue)]
    public void CalculateAddWithTheoryAndInlineData_ReturnAddValue(int val1, int val2, int expected)
    {
        // Arrenge
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(val1, val2);

        // Assert
        Assert.Equal(expected, result);
    }


    /***
     * Parameterised Test Using a dedicated data class with [ClassData]
     * If the values you need to pass to your [Theory] test aren't constants,
     * then you can use an alternative attribute, [ClassData], to provide the parameters.
     * This attribute takes a Type which xUnit will use to obtain the data:
     */
    [Theory]
    [ClassData(typeof(CalculatorTestData))]
    public void CalculateAddWithTheoryAndClassData_ReturnAddValue(int val1, int val2, int expected)
    {
        // Arrenge
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(val1, val2);

        // Assert
        Assert.Equal(expected, result);
    }


    /***
     * Parameterised Test Using generator properties with the [MemberData] properties
     * The [MemberData] attribute can be used to fetch data for a [Theory]
     * from a static property or method of a type. This attribute has quite
     * a lot options, so I'll just run through some of them here.
     */
    [Theory]
    [MemberData(nameof(testData))]
    public void CalculateAddWithTheoryAndMemberData_ReturnAddValue(int val1, int val2, int expected)
    {
        // Arrenge
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(val1, val2);

        // Assert
        Assert.Equal(expected, result);
    }


    /***
     * Parameterised Test Loading data from a method on the test class
     * As well as properties, you can obtain [MemberData] from a static method.
     * These methods can even be parameterised themselves. If that's the case,
     * you need to supply the parameters (takes only provided no. of ) in the [MemberData], as shown below:
     */
    [Theory]
    [MemberData(nameof(GetTestData), parameters: 3)]
    public void CalculateAddWithTheoryAndMethodData_ReturnAddValue(int val1, int val2, int expected)
    {
        // Arrenge
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(val1, val2);

        // Assert
        Assert.Equal(expected, result);
    }


    /***
     * Parameterised Test Loading data from a property or method on a different class
     * 
     * This option is sort of a hybrid between the [ClassData] attribute and
     * the [MemberData] attribute usage you've seen so far. Instead of loading
     * data from a property or method on the test class, you load data from a
     * property or method on some other specified type:
     */
    [Theory]
    [MemberData(nameof(CalculatorDataModel.GetData), MemberType = typeof(CalculatorDataModel))]
    public void CalculateAddWithTheoryAndMethodOrClassData_ReturnAddValue(int val1, int val2, int expected)
    {
        // Arrenge
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(val1, val2);

        // Assert
        Assert.Equal(expected, result);
    }


    /***
     * The [MemberData] attribute can load data from an IEnnumerable<object[]> property
     * on the test class. The xUnit analyzers will pick up any issues with your configuration,
     * such as missing properties, or using properties that return invalid types.
     * 
     * In the following example I've added a testData property which returns an IEnumerable<object[]>,
     * just like for the [ClassData]
     */
    public static readonly IEnumerable<object[]> testData =
    [
        [ 5, 6, 11],
        [ 50, 56, 106 ],
        [ 51, 60, 111 ],
        [ -84, 90, 6 ],
        [ 117, -33, 84 ],
        [ 150, -75, 75 ],
        [ int.MinValue, -1, int.MaxValue ],
        [ int.MaxValue, 1, int.MinValue]
    ];

    // This method parameter numTests means 
    public static IEnumerable<object[]> GetTestData(int numTests)
    {
        return testData.Take(numTests);
    }
}


/***
 * We've specified a type of CalculatorTestData in the [ClassData] attribute.
 * This class must implement IEnumerable<object[]>, where each item returned
 * is an array of objects to use as the method parameters. We could rewrite
 * the data from the [InlineData] attribute using this approach:
 */

public class CalculatorTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 5, 6, 11 };
        yield return new object[] { 50, 56, 106 };
        yield return new object[] { 51, 60, 111 };
        yield return new object[] { -84, 90, 6 };
        yield return new object[] { 117, -33, 84 };
        yield return new object[] { 150, -75, 75 };
        yield return new object[] { int.MinValue, -1, int.MaxValue };
        yield return new object[] { int.MaxValue, 1, int.MinValue };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Creating a separate class with method of GetData
public class CalculatorDataModel
{
    public static IEnumerable<object[]> GetData =>
    [
        [ 5, 6, 11],
        [ 50, 56, 106 ],
        [ 51, 60, 111 ],
        [ -84, 90, 6 ],
        [ 117, -33, 84 ],
        [ 150, -75, 75 ],
        [ int.MinValue, -1, int.MaxValue ],
        [ int.MaxValue, 1, int.MinValue]
    ];
}