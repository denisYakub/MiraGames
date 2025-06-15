using MiraGames.Server.Entities;

namespace MiraGames.Server.Interfaces
{
    public interface ITokenService<T>
    {
        public string GenerateToken(T user);
    }
}
