namespace BlazorWeb.Data
{
    public class UTCDemo : IDemo, IUTCDemo
    {
        public DateTime StartupTime { get; set; }

        public UTCDemo()
        {
            StartupTime = DateTime.UtcNow;
        }
    }
}
