using System;
using WebArt.Domain.Common;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        
        public User Sender { get; private set; } = null!;
        public User Receiver { get; private set; } = null!;
        
        private Message() { }
        
        private Message(Guid senderId, Guid receiverId, string content)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
            IsRead = false;
        }
        
        public static Message Create(Guid senderId, Guid receiverId, string content)
        {
            if (senderId == Guid.Empty)
                throw new BusinessRuleViolationException("Sender ID cannot be empty");
                
            if (receiverId == Guid.Empty)
                throw new BusinessRuleViolationException("Receiver ID cannot be empty");
                
            if (senderId == receiverId)
                throw new BusinessRuleViolationException("Cannot send message to yourself");
                
            if (string.IsNullOrWhiteSpace(content))
                throw new BusinessRuleViolationException("Message content cannot be empty");
                
            return new Message(senderId, receiverId, content);
        }
        
        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}