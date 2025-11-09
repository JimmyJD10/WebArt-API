using System;

namespace WebArt.Domain.Exceptions
{
    public class BusinessRuleViolationException : DomainException
    {
        public BusinessRuleViolationException(string rule)
            : base($"Business rule violated: {rule}")
        {
        }
        
        public BusinessRuleViolationException(string rule, Exception innerException)
            : base($"Business rule violated: {rule}", innerException)
        {
        }
    }
}