using MiraGames.Server.Database.Contexts;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Database.Repositories
{
    public class RateRepository(PostgresContext context) : IRepository<Rate>
    {
        public void Add(Rate entity) => 
            context.Rates.Add(entity);

        public Rate Get(int id)
        {
            var rate = context.Rates.FirstOrDefault(u => u.Id == id);

            return rate ?? throw new BadHttpRequestException($"No rate with this id: {id}");
        }

        public Rate Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetAll() =>
            context.Rates;

        public void Save() =>
            context.SaveChanges();

        public void Update(Rate entity) =>
            context.Rates.Update(entity);
    }
}
