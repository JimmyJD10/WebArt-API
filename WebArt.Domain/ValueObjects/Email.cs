using System;

namespace WebArt.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        
        private Email(string value)
        {
            Value = value;
        }
        
        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");
                
            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format");
                
            return new Email(email.ToLowerInvariant().Trim());
        }
        
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
        public override string ToString() => Value;
        
        public override bool Equals(object? obj)
        {
            if (obj is Email other)
                return Value == other.Value;
            return false;
        }
        
        public override int GetHashCode() => Value.GetHashCode();
    }
}