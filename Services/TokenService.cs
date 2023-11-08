using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using LoginModel.Models;
using UserData;
using PasswordHasher;


public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly POSDbContext _context;
    public TokenService(IConfiguration configuration, POSDbContext context)
    {
        _configuration = configuration;
        _context = context;
        
    }

    public string GenerateToken(Login model)
    {
        var user = GetUserFromDatabase(model.Username); // retrieve the user based on the username
        if (user == null || !ValidateUserPassword(model.Password, user.PasswordHash, user.PasswordSalt))
        {
            // Return 401 Unauthorized if the user doesn't exist or password is incorrect
            return null;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var newSecretKey = "IZ2tn4yvOsfBY03ez2v0JPFjkfL8/Ru0VP1314csN2QiNI8onuSBMQqbnC+4hUXKSuaP0DAuPfDDWdE8JtS7864JPww4/tYwK7yPurRJP1CeA00vPwXNQIBD0eXDA3cjTOJylldhMU0T/jpXYHWwn6w1npsF7f5ows8tGA6aLgpaFZFX2TQN9BEVdLbGEW4h";
        var key = Encoding.UTF8.GetBytes(newSecretKey);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Email, user.Email),
            
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JwtSettings:ExpirationHours"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JwtSettings:ValidIssuer"],
            Audience = _configuration["JwtSettings:ValidAudience"],
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

     private User GetUserFromDatabase(string username)
    {
        return _context.Users.SingleOrDefault(u => u.Username == username);
    }

     private bool ValidateUserPassword(string password, string storedHash, byte[] storedSalt)
    {
        
        return PasswordHasher1.VerifyPassword(password, storedHash);
    }
}
