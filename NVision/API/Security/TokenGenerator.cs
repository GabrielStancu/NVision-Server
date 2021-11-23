using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Security
{
    public static class TokenGenerator
    {
        public static string GenerateToken(string username, UserType userType)
        {
            string jwtKey = ConfigurationManager.GetKey();
            string issuer = ConfigurationManager.GetIssuer();
            string audience = ConfigurationManager.GetAudience();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, userType.ToString())
        };
            var tokeOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

    }
}
