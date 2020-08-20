using AutoMapper.Configuration;
using GraphQLCoreAPI.Authentication.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.Authentication
{
    public class JwtAuthenticationHandler: IJwtAuthenticationHandler
    {
        private string _key;
        private int _size;
        private int _expiryTimeInMins;
        public JwtAuthenticationHandler(string Key, int size, int expiryTimeInMins)
        {
            _key = Key;
            _size = size;
            _expiryTimeInMins = expiryTimeInMins;
        }

        public Tuple<string,DateTime> GenerateJwtToken(Claim[] claims)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

            DateTime tokenExpiry = DateTime.UtcNow.AddMinutes(_expiryTimeInMins);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                Expires = tokenExpiry
            };

            return Tuple.Create<string, DateTime>(jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(securityTokenDescriptor)), tokenExpiry);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[_size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}
