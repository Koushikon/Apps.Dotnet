namespace Basic;

[TestClass]
public class TestClass2
{
    [TestInitialize]
    public static void ClassInitialize()
    {
        Console.WriteLine("Inside ClassInitialize");
    }

    [TestMethod]
    public void TestMethod2()
    {
        Console.WriteLine("Inside TestMethod-2");
    }

    [TestCleanup]
    public static void ClassCleanup()
    {
        Console.WriteLine("Inside ClassCleanup");
    }
}