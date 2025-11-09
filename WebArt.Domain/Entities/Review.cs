using System;
using WebArt.Domain.Common;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid ReviewerId { get; private set; }
        public Guid ReviewedUserId { get; private set; }
        public Guid OrderId { get; private set; }
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        
        public User Reviewer { get; private set; } = null!;
        public User ReviewedUser { get; private set; } = null!;
        public Order Order { get; private set; } = null!;
        
        private Review() { }
        
        private Review(Guid reviewerId, Guid reviewedUserId, Guid orderId, int rating, string? comment)
        {
            ReviewerId = reviewerId;
            ReviewedUserId = reviewedUserId;
            OrderId = orderId;
            Rating = rating;
            Comment = comment;
        }
        
        public static Review Create(Guid reviewerId, Guid reviewedUserId, Guid orderId, int rating, string? comment)
        {
            if (reviewerId == Guid.Empty)
                throw new BusinessRuleViolationException("Reviewer ID cannot be empty");
                
            if (reviewedUserId == Guid.Empty)
                throw new BusinessRuleViolationException("Reviewed user ID cannot be empty");
                
            if (reviewerId == reviewedUserId)
                throw new BusinessRuleViolationException("Cannot review yourself");
                
            if (orderId == Guid.Empty)
                throw new BusinessRuleViolationException("Order ID cannot be empty");
                
            if (rating < 1 || rating > 5)
                throw new BusinessRuleViolationException("Rating must be between 1 and 5");
                
            return new Review(reviewerId, reviewedUserId, orderId, rating, comment);
        }
        
        public void UpdateReview(int rating, string? comment)
        {
            if (rating < 1 || rating > 5)
                throw new BusinessRuleViolationException("Rating must be between 1 and 5");
                
            Rating = rating;
            Comment = comment;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}