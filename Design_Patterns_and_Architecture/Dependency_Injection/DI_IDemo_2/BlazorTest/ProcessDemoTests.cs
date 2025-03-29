using BlazorWeb.Data;

namespace BlazorTest
{
    public class ProcessDemoTests
    {
        [Fact]
        public void GetDaysMonth_CheckLeapYear()
        {
            TestingDemo td = new(DateTime.Parse("2/2/2000"));
            ProcessDemo pd = new(td);

            int expected = 29;
            int actual = pd.GetDaysMonth();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDaysMonth_CheckNotLeapYear()
        {
            TestingDemo td = new(DateTime.Parse("2/2/1999"));
            ProcessDemo pd = new(td);

            int expected = 28;
            int actual = pd.GetDaysMonth();

            Assert.Equal(expected, actual);
        }


        public class TestingDemo : IDemo
        {
            public DateTime StartupTime { get; set; }

            public TestingDemo(DateTime startTime)
            {
                StartupTime = startTime;
            }
        }
    }
}