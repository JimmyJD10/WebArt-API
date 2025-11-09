using System;

namespace WebArt.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        
        private Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }
        
        public static Address Create(string street, string city, string state, string country, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty");
                
            return new Address(
                street.Trim(),
                city.Trim(),
                state?.Trim() ?? string.Empty,
                country.Trim(),
                postalCode?.Trim() ?? string.Empty
            );
        }
        
        public override string ToString()
        {
            var parts = new[] { Street, City };
            if (!string.IsNullOrEmpty(State))
                return $"{Street}, {City}, {State} {PostalCode}, {Country}";
            return $"{Street}, {City} {PostalCode}, {Country}";
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is Address other)
                return Street == other.Street && 
                       City == other.City && 
                       Country == other.Country &&
                       PostalCode == other.PostalCode;
            return false;
        }
        
        public override int GetHashCode() => HashCode.Combine(Street, City, Country, PostalCode);
    }
}