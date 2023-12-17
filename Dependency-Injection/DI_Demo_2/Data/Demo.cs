namespace DI_Demo_2.Data
{
    public class Demo
    {
        public DateTime StartupTime { get; set; }

        public Demo()
        {
            StartupTime = DateTime.Now;
        }
    }
}
