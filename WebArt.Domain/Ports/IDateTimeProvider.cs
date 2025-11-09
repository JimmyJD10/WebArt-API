using System;

namespace WebArt.Domain.Ports
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
    }
}