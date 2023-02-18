namespace BlazorWeb.Data
{
    public class Demo : IDemo
    {
        public DateTime StartupTime { get; set; }

        public Demo()
        {
            StartupTime = DateTime.Now;
        }
    }
}
