using System.ComponentModel.DataAnnotations.Schema;
using MiraGames.Server.Entities.Enums;

namespace MiraGames.Server.Entities
{
    [Table("rates")]
    public class Rate
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Value 
        { 
            get {  return _value; }
            set { _value = value; }
        }

        private decimal _value;

        public Rate()
        {
            Id = -1;
            Name = string.Empty;
            _value = 0;
        }

        public Rate(Currencies currency, decimal value)
        {
            Id = (int)currency;
            Name = currency.ToString();
            _value = value;
        }

        public Rate(int id, string name, decimal value)
        {
            Id = id;
            Name = name;
            _value = value;
        }
    }
}
