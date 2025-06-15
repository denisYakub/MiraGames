using Microsoft.AspNetCore.Mvc;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;

namespace MiraGames.Server.Services
{
    public class PaymentService(IRepository<Payment> repository) : IPaymentService
    {
        public IEnumerable<Payment> GetPayments(Guid id, int lastN)
        {

            return 
                repository
                .GetAll()
                .Where(p => p.UserId == id)
                .TakeLast(lastN);
        }
    }
}
