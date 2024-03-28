using ECommerce.Application.IServices;
using ECommerce.Application.Models.VMs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Application.Services
{
    public class TokenService(IConfiguration configuration, IOptions<JwtTokenSettings> jwtTokenSettings) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtTokenSettings _jwtTokenSettings = jwtTokenSettings.Value;

        public string GenerateAccessToken(UserVM userVM)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userVM.Username),
                new Claim(ClaimTypes.Email, userVM.Email),
                // Add other claims as needed, e.g., user ID, roles, etc.
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenSettings.Issuer,
                audience: _jwtTokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtTokenSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = GetTokenValidationParameters();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = _jwtTokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtTokenSettings.Audience,
                ValidateLifetime = false // Token expiration time is checked separately
            };
        }
    }
}
