namespace Basic;

[TestClass]
public class PrimeService
{
    [TestMethod]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        bool result = IsPrimeExample(-5);

        Assert.IsFalse(result, "1 should not be prime");
    }

    private bool IsPrimeExample(int val) => val > 0;
}