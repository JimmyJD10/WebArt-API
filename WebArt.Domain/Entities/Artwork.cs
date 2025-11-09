using System;
using WebArt.Domain.Common;
using WebArt.Domain.Enums;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class Artwork : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public Visibility Visibility { get; private set; }
        public Guid ArtistId { get; private set; }
        public Guid? PortfolioId { get; private set; }
        
        public User Artist { get; private set; } = null!;
        public Portfolio? Portfolio { get; private set; }
        
        private Artwork() { }
        
        private Artwork(string title, string description, string imageUrl, Visibility visibility, Guid artistId)
        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Visibility = visibility;
            ArtistId = artistId;
        }
        
        public static Artwork Create(string title, string description, string imageUrl, Visibility visibility, Guid artistId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessRuleViolationException("Title cannot be empty");
                
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new BusinessRuleViolationException("Image URL cannot be empty");
                
            if (artistId == Guid.Empty)
                throw new BusinessRuleViolationException("Artist ID cannot be empty");
                
            return new Artwork(title, description ?? string.Empty, imageUrl, visibility, artistId);
        }
        
        public void UpdateDetails(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessRuleViolationException("Title cannot be empty");
                
            Title = title;
            Description = description ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void ChangeVisibility(Visibility newVisibility)
        {
            Visibility = newVisibility;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void AssignToPortfolio(Guid portfolioId)
        {
            PortfolioId = portfolioId;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void RemoveFromPortfolio()
        {
            PortfolioId = null;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public bool IsPublic() => Visibility == Visibility.Public;
    }
}