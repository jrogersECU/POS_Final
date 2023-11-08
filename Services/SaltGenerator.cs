using System.Security.Cryptography;

public static class SaltGenerator
{
    public static byte[] GenerateRandomSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
}
