using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.AppCore.Services;

public class TokenService(
    IConfiguration configuration) : ITokenService
{
    public string GenerateToken(User user, IList<Claim> claims)
    {
       var secret = configuration["Jwt:Secret"];
       
       if(string.IsNullOrEmpty(secret))
           return string.Empty;
       
       var key = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(secret));

       var token = new JwtSecurityToken(
           issuer: configuration["Jwt:Issuer"],
           audience: configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(
               double.Parse(configuration["Jwt:ExpiresInMinutes"]!)),
           signingCredentials: new SigningCredentials(
               key, SecurityAlgorithms.HmacSha256)
       );
       
       return new JwtSecurityTokenHandler().WriteToken(token);
    }
}