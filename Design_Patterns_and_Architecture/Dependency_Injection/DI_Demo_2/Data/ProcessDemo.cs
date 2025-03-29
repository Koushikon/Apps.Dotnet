namespace DI_Demo_2.Data
{
    public class ProcessDemo
    {
        private readonly Demo _demo;

        // With Constructor Injection of Demo Class
        public ProcessDemo(Demo demo)
        {
            _demo = demo;
        }

        public int GetDaysMonth()
        {
            return _demo.StartupTime.Month switch
            {
                1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
                4 or 6 or 9 or 11 => 30,
                2 => 28,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }
}
