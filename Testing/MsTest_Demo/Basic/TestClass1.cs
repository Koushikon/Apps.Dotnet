namespace Basic;

[TestClass]
public class Initialize
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        Console.WriteLine("Inside AssemblyInitialize");
    }
}

public class Deinitialize()
{
    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Console.WriteLine("Inside AssemblyCleanup");
    }
}

[TestClass]
public class TestClass1
{
    [ClassInitialize]
    public static void ClassInitialize()
    {
        Console.WriteLine("Inside ClassInitialize");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Console.WriteLine("Inside ClassCleanup");
    }

    [TestMethod]
    public void TestMethod1()
    {
        Console.WriteLine("Inside TestMethod 1");
    }
}