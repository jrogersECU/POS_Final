using BCrypt.Net;

public class PasswordHasher
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return (hash, salt);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
        
    }
}