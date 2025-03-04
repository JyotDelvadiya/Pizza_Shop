using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PizzaShop.Data.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using PizzaShop.Business.Interface;
namespace PizzaShop.Business.Services;

public class GenerateJwt : IGenerateJwt
{
    private readonly IConfiguration _configuration;
    public GenerateJwt(IConfiguration configuration)
    {
        _configuration=configuration;
    }
    public string GenerateJwtToken(Account user,string role, TimeSpan? expirationTime = null)
    {
       var claims = new List<Claim>
            {
                // // Subject (sub) claim with the user's ID
                new Claim(JwtRegisteredClaimNames.Sub, user.Accountid.ToString()),
                // // JWT ID (jti) claim with a unique identifier for the token
                // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Email claim with the user's email
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

        var jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT key is not configured.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ICZasdklfjhacknHLisdfnLsdhoSfal4k2342s134k2dfcna234023q42e4w4rIFAKn"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiryTime = DateTime.Now.Add(expirationTime ?? TimeSpan.FromMinutes(30));

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7095/",
            audience: "https://localhost:7095/",
            claims: claims,
            expires: expiryTime,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetAccountidFromJwtToken(string token)
    {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                throw new ArgumentException("Invalid JWT token");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                throw new ArgumentException("JWT token does not contain a user ID");
            }

            if (!int.TryParse(userIdClaim.Value, out int Accountid))
            {
                throw new ArgumentException("Invalid user ID in JWT token");
            }
            return Accountid;
    }
}