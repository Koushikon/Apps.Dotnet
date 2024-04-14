namespace xTest
{
	public class UnitTest1
	{
		[Fact]
		public void TestMethodForAllFrameworks()
		{
			Console.WriteLine("Hello to everyone.");

			Assert.True(true);
		}

#if NET7_0
		[Fact]
		public void TestMethodForNet7()
		{
			Console.WriteLine("Hello from .Net 7 Framework");

			Assert.True(true);
		}
#elif NET6_0

		[Fact]
		public void TestMethodForNet6()
		{
			Console.WriteLine("Hello from .Net 6 Framework");

			Assert.True(true);
		}
#endif
	}
}