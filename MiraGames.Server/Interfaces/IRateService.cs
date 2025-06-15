using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Interfaces
{
    public interface IRateService
    {
        public decimal Get(Currencies currency);
        public void Update(Currencies currency, decimal newValue);
    }
}
