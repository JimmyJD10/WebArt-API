using System;

namespace WebArt.Domain.Common
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}