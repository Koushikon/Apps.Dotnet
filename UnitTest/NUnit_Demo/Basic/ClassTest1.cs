namespace Basic;

public class ClassTest1
{
    [OneTimeSetUp]
    public void ClassInitialize()
    {
        Console.WriteLine("Inside Onetime Setup");
    }

    [OneTimeTearDown]
    public void ClassCleanup()
    {
        Console.WriteLine("Inside Onetime Teardown!");
    }

    [Test]
    public void TestMethod3()
    {
        Console.WriteLine("Inside from Test method 3");
    }

    [Test]
    public void TestMethod4()
    {
        Console.WriteLine("Inside from Test method 4");
    }
}