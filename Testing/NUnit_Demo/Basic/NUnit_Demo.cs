namespace Basic;

public class NUnit_Demo
{
    /***
     * Contains the initialization code, which is triggered before every test case
     */
    [SetUp]
    public void Initialize()
    {
        Console.WriteLine("Inside Setup");
    }

    /***
     * Contains the cleanup code, which is triggered after every test case
     */
    [TearDown]
    public void Cleanup()
    {
        Console.WriteLine("Inside Teardown!");
    }


    [Test, Order(2)]
    public void TestMethod1()
    {
        Console.WriteLine("Inside from Test method 1");
    }

    [Test, Order(1)]
    public void TestMethod2()
    {
        Console.WriteLine("Inside from Test method 2");
    }
}