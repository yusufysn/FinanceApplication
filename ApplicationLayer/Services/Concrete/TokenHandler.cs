using ApplicationLayer.DTOs;
using ApplicationLayer.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Concrete
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(string userId, string username, int minute)
        {
            DTOs.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration.GetSection("Token")["SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.Now.AddSeconds(minute);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };
            JwtSecurityToken securityToken = new(
                audience: _configuration.GetSection("Token")["Audience"],
                issuer: _configuration.GetSection("Token")["Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler _tokenHandler = new();
            token.AccessToken = _tokenHandler.WriteToken(securityToken);
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
