using System;
using WebArt.Domain.Common;
using WebArt.Domain.Enums;
using WebArt.Domain.ValueObjects;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public Guid CustomerId { get; private set; }
        public Guid ArtistId { get; private set; }
        public string Description { get; private set; }
        public decimal PriceAmount { get; private set; }
        public string PriceCurrency { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        
        public User Customer { get; private set; } = null!;
        public User Artist { get; private set; } = null!;
        
        private Order() { }
        
        private Order(Guid customerId, Guid artistId, string description, Money price, DateTime? deliveryDate)
        {
            CustomerId = customerId;
            ArtistId = artistId;
            Description = description;
            PriceAmount = price.Amount;
            PriceCurrency = price.Currency;
            Status = OrderStatus.Pending;
            DeliveryDate = deliveryDate;
        }
        
        public static Order Create(Guid customerId, Guid artistId, string description, decimal amount, string currency = "USD", DateTime? deliveryDate = null)
        {
            if (customerId == Guid.Empty)
                throw new BusinessRuleViolationException("Customer ID cannot be empty");
                
            if (artistId == Guid.Empty)
                throw new BusinessRuleViolationException("Artist ID cannot be empty");
                
            if (customerId == artistId)
                throw new BusinessRuleViolationException("Customer and Artist cannot be the same");
                
            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessRuleViolationException("Order description cannot be empty");
                
            var price = Money.Create(amount, currency);
            
            return new Order(customerId, artistId, description, price, deliveryDate);
        }
        
        public Money GetPrice() => Money.Create(PriceAmount, PriceCurrency);
        
        public void Accept()
        {
            if (Status != OrderStatus.Pending)
                throw new BusinessRuleViolationException("Only pending orders can be accepted");
                
            Status = OrderStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void Complete()
        {
            if (Status != OrderStatus.InProgress)
                throw new BusinessRuleViolationException("Only in-progress orders can be completed");
                
            Status = OrderStatus.Completed;
            CompletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
                throw new BusinessRuleViolationException("Cannot cancel completed orders");
                
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void UpdateDeliveryDate(DateTime newDeliveryDate)
        {
            if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
                throw new BusinessRuleViolationException("Cannot update delivery date for completed or cancelled orders");
                
            DeliveryDate = newDeliveryDate;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public bool IsActive() => Status == OrderStatus.Pending || Status == OrderStatus.InProgress;
    }
}