using MiraGames.Server.Entities;
using MiraGames.Server.Entities.Enums;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Services
{
    public class RateService(IRepository<Rate> repository) : IRateService
    {
        public decimal Get(Currencies currency)
        {
            var rate = repository.Get((int)currency);

            return rate.Value;
        }

        public void Update(Currencies currency, decimal newValue)
        {
            var rate = repository.Get((int)currency);

            rate.Value = newValue;

            repository.Update(rate);

            repository.Save();
        }
    }
}
