using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; init; }
        [Column("login")]
        public string Email { get; private set; }
        [Column("password")]
        public string Password { get; private set; }
        public ICollection<Payment> Payments { get; init; }

        public User()
        {
            Id = Guid.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Payments = new List<Payment>(10);
        }
        public User(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Payments = new List<Payment>(10);
        }
        public User(Guid id, string email, string password, IEnumerable<Payment> payments) 
        { 
            Id = id;
            Email = email;
            Password = password;
            Payments = [.. payments];
        }

        public void AddPayment(
            decimal price, Currencies currency, 
            PaymentStatuses status = PaymentStatuses.Unpaid
        )
        {
            var payment = new Payment(Id, price, currency, status);

            Payments.Add(payment);
        }
    }
}
