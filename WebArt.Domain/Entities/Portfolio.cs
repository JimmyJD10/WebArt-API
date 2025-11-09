using System;
using System.Collections.Generic;
using System.Linq;
using WebArt.Domain.Common;
using WebArt.Domain.Exceptions;

namespace WebArt.Domain.Entities
{
    public class Portfolio : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public Guid ArtistId { get; private set; }
        
        public User Artist { get; private set; } = null!;
        private readonly List<Artwork> _artworks = new();
        public IReadOnlyCollection<Artwork> Artworks => _artworks.AsReadOnly();
        
        private Portfolio() { }
        
        private Portfolio(string title, string? description, Guid artistId)
        {
            Title = title;
            Description = description;
            ArtistId = artistId;
        }
        
        public static Portfolio Create(string title, string? description, Guid artistId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessRuleViolationException("Portfolio title cannot be empty");
                
            if (artistId == Guid.Empty)
                throw new BusinessRuleViolationException("Artist ID cannot be empty");
                
            return new Portfolio(title, description, artistId);
        }
        
        public void UpdateDetails(string title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessRuleViolationException("Portfolio title cannot be empty");
                
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void AddArtwork(Artwork artwork)
        {
            if (artwork.ArtistId != ArtistId)
                throw new BusinessRuleViolationException("Cannot add artwork from a different artist");
                
            if (_artworks.Any(a => a.Id == artwork.Id))
                throw new BusinessRuleViolationException("Artwork already exists in this portfolio");
                
            _artworks.Add(artwork);
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void RemoveArtwork(Guid artworkId)
        {
            var artwork = _artworks.FirstOrDefault(a => a.Id == artworkId);
            if (artwork == null)
                throw new NotFoundException("Artwork", artworkId);
                
            _artworks.Remove(artwork);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}