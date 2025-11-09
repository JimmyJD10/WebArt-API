using WebArt.Domain.Enums;

namespace WebArt.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public RoleType Role { get; set; } = RoleType.Client;
    
    public string? FullName { get; set; }
}