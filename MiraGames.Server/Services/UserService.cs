using MiraGames.Server.Entities;
using MiraGames.Server.Entities.Enums;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Services
{
    public class UserService(IRepository<User> repository, ITokenService<User> token) : IUserService
    {
        public string CreateUserAndToken(string login, string password)
        {
            var sameLogin = 
                repository
                .GetAll()
                .FirstOrDefault(u => u.Email == login);        

            if (sameLogin != null)
                throw new BadHttpRequestException("Login already exists");

            var user = new User(login, password);

            repository.Add(user);

            repository.Save();

            return token.GenerateToken(user);
        }

        public IEnumerable<User> GetAll() => 
            repository.GetAll();
    }
}
