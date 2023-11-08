using BCrypt.Net;
using System.Text;

namespace PasswordHasher{
public class PasswordHasher1
{
    public static string HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hash;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
}