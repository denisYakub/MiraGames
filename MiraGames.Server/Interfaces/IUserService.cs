using MiraGames.Server.Entities;
using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Interfaces
{
    public interface IUserService
    {
        string CreateUserAndToken(string login, string password);
        IEnumerable<User> GetAll();
    }
}
