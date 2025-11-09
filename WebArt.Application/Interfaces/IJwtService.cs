using WebArt.Domain.Entities;

namespace WebArt.Application.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}