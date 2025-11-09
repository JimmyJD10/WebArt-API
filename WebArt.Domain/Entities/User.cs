using System;
using System.Collections.Generic;
using System.Linq;
using WebArt.Domain.Common;
using WebArt.Domain.Enums;
using WebArt.Domain.ValueObjects;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string EmailValue { get; private set; }
        public string PasswordHash { get; private set; }
        public RoleType Role { get; private set; }
        public string? Biography { get; private set; }
        public string? ProfileImageUrl { get; private set; }
        
        // Navigation properties
        private readonly List<Artwork> _artworks = new();
        private readonly List<Portfolio> _portfolios = new();
        private readonly List<Order> _ordersAsCustomer = new();
        private readonly List<Order> _ordersAsArtist = new();
        private readonly List<Review> _reviewsGiven = new();
        private readonly List<Review> _reviewsReceived = new();
        private readonly List<Message> _sentMessages = new();
        private readonly List<Message> _receivedMessages = new();
        
        public IReadOnlyCollection<Artwork> Artworks => _artworks.AsReadOnly();
        public IReadOnlyCollection<Portfolio> Portfolios => _portfolios.AsReadOnly();
        public IReadOnlyCollection<Order> OrdersAsCustomer => _ordersAsCustomer.AsReadOnly();
        public IReadOnlyCollection<Order> OrdersAsArtist => _ordersAsArtist.AsReadOnly();
        public IReadOnlyCollection<Review> ReviewsGiven => _reviewsGiven.AsReadOnly();
        public IReadOnlyCollection<Review> ReviewsReceived => _reviewsReceived.AsReadOnly();
        public IReadOnlyCollection<Message> SentMessages => _sentMessages.AsReadOnly();
        public IReadOnlyCollection<Message> ReceivedMessages => _receivedMessages.AsReadOnly();
        
        private User() { }
        
        private User(string name, string email, string passwordHash, RoleType role)
        {
            Name = name;
            EmailValue = email;
            PasswordHash = passwordHash;
            Role = role;
        }
        
        public static User Create(string name, string email, string passwordHash, RoleType role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRuleViolationException("Name cannot be empty");
                
            var validEmail = Email.Create(email);
            
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new BusinessRuleViolationException("Password hash cannot be empty");
                
            return new User(name, validEmail.Value, passwordHash, role);
        }
        
        public void UpdateProfile(string? biography, string? profileImageUrl)
        {
            Biography = biography;
            ProfileImageUrl = profileImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void ChangeRole(RoleType newRole)
        {
            Role = newRole;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public bool IsArtist() => Role == RoleType.Artist || Role == RoleType.Admin;
        
        public bool CanCreateArtwork() => IsArtist();
    }
}