using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Model;
using WebApi.Service;

namespace WebApi.Controllers
{
    public class UsersController : Controller
    {
        // Learn from Nick chapsas
        
        private readonly UsersService _usersService;
        private readonly IHashids _hashIds;

        public UsersController(UsersService usersService, IHashids hashIds)
        {
            _usersService = usersService;
            _hashIds = hashIds;
        }

        [HttpGet("int/{id:int}")]
        public IEnumerable<User> Get([FromRoute] int id)
        {
            return Enumerable.Range(1, 5).Select(index => new User
            {
                Id = id,
                GuId = _usersService.GetNewGuId(),
                HashIds = _hashIds.Encode(12345),
                Name ="Koushik",
                Age = 71
            })
            .ToArray();
        }


        [HttpGet("guid/{id:guid}")]
        public IEnumerable<User> Get([FromRoute] Guid id)
        {
            return Enumerable.Range(1, 5).Select(index => new User
            {
                Id = _usersService.GetNewId(),
                GuId = id,
                HashIds = _hashIds.Encode(12345),
                Name = "Koushik",
                Age = 91
            })
            .ToArray();
        }


        [HttpGet("hashid/{id}")]
        public IEnumerable<User> Get([FromRoute] string id)
        {
            var rawId = _hashIds.Decode(id);
            var hash = _hashIds.Encode(12345);

            return Enumerable.Range(1, 5).Select(index => new User
            {
                Id = _usersService.GetNewId(),
                GuId = _usersService.GetNewGuId(),
                HashIds = string.Join(", ", rawId),
                Name = "Koushik",
                Age = 111
            })
            .ToArray();
        }
    }
}
