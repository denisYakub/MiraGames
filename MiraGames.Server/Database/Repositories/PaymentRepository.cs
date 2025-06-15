using MiraGames.Server.Database.Contexts;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Database.Repositories
{
    public class PaymentRepository(PostgresContext context) : IRepository<Payment>
    {
        public void Add(Payment entity) =>
            context.Payments.Add(entity);

        public Payment Get(int id)
        {
            throw new NotImplementedException();
        }

        public Payment Get(Guid id)
        {
            var payment = context.Payments.FirstOrDefault(u => u.Id == id);

            return payment ?? throw new BadHttpRequestException($"No payment with this id: {id}");
        }

        public IEnumerable<Payment> GetAll() =>
            context.Payments;

        public void Save() =>
            context.SaveChanges();

        public void Update(Payment entity) =>
            context.Payments.Update(entity);
    }
}
