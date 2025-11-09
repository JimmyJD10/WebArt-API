using System;

namespace WebArt.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        
        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        
        public static Money Create(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");
                
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty");
                
            return new Money(amount, currency.ToUpperInvariant());
        }
        
        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies");
                
            return new Money(Amount + other.Amount, Currency);
        }
        
        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot subtract money with different currencies");
                
            return new Money(Amount - other.Amount, Currency);
        }
        
        public override string ToString() => $"{Amount:N2} {Currency}";
        
        public override bool Equals(object? obj)
        {
            if (obj is Money other)
                return Amount == other.Amount && Currency == other.Currency;
            return false;
        }
        
        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }
}