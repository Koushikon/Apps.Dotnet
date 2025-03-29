namespace WebApi.Model
{
    public class User
    {
        public int Id { get; set; }
        public Guid GuId { get; set; }
        public string HashIds { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
