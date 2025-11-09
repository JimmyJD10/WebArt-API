using System.Security.Cryptography;
using System.Text;
using WebArt.Application.Interfaces;

namespace WebArt.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize  = 32;
    private const int Iterations = 100_000;
    private const char Delim = '.';

    public string Hash(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        var hash = PBKDF2(password, salt, Iterations, KeySize);

        return $"v1{Delim}{Iterations}{Delim}{Convert.ToBase64String(salt)}{Delim}{Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        var parts = passwordHash.Split(Delim, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 4 || parts[0] != "v1") return false;

        var iterations = int.Parse(parts[1]);
        var salt = Convert.FromBase64String(parts[2]);
        var stored = Convert.FromBase64String(parts[3]);

        var computed = PBKDF2(password, salt, iterations, stored.Length);
        return CryptographicOperations.FixedTimeEquals(stored, computed);
    }

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int length)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256
        );
        return pbkdf2.GetBytes(length);
    }
}