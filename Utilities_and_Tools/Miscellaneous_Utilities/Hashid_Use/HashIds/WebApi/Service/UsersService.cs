namespace WebApi.Service
{
    public class UsersService
    {
        public int GetNewId()
        {
            Random rnd = new();
            return rnd.Next();
        }

        public Guid GetNewGuId()
        {
            return Guid.NewGuid();
        }
    }
}
