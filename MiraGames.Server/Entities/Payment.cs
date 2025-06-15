using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Entities
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [ForeignKey("users")]
        [Column("user_id")]
        public Guid UserId { get; init; }
        public User User { get; init; } = null!;
        [Column("date")]
        public DateTime Date { get; init; }

        [Column("price")]
        public decimal Price { get; init; }
        [Column("currency")]
        public Currencies Currency { get; init; }
        [Column("status")]
        public PaymentStatuses Status { get; private set; }

        public Payment()
        {
            Id = Guid.Empty;
            UserId = Guid.Empty;
            Date = DateTime.UtcNow;
            Price = decimal.Zero;
        }
        public Payment(Guid userId, decimal price, Currencies currency, PaymentStatuses status)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Date = DateTime.UtcNow;
            Price = price;
            Currency = currency;
            Status = status;
        }
        public Payment(Guid id, Guid userId, DateTime date, decimal price, Currencies currency, PaymentStatuses status)
        {
            Id = id;
            UserId = userId;
            Date = date;
            Price = price;
            Currency = currency;
            Status = status;
        }
    }
}
