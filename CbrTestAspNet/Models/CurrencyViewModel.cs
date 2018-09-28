using CbrTestAspNet.Domain;

namespace CbrTestAspNet.Models
{
    public class CurrencyViewModel
    {
        public string Name { get; set; }

        public int Nominal { get; set; }

        public decimal Value { get; set; }

        public static CurrencyViewModel Create(Currency currency)
        {
            return new CurrencyViewModel
            {
                Name = $"{currency.Name} ({currency.CharCode})",
                Value = currency.Value,
                Nominal = currency.Nominal
            };
        }
    }
}