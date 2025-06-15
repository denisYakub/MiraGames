using MiraGames.Server.Entities.Enums;
using MiraGames.Server.Entities;

namespace MiraGames.Server.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPayments(Guid id, int lastN);
    }
}
