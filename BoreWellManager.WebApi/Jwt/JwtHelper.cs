using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoreWellManager.WebApi.Jwt
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(JwtDto jwtInfo)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecretKey));

            //kimlik bilgileri için
            var credentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtClaimName.Id,jwtInfo.Id.ToString()),
                new Claim(JwtClaimName.Name,jwtInfo.Name),
                new Claim(JwtClaimName.Phone,jwtInfo.Phone),
                new Claim(JwtClaimName.UserType,jwtInfo.UserType.ToString()),
                new Claim(JwtClaimName.IsResponsible,jwtInfo.IsResponsible.ToString()),
                new Claim(ClaimTypes.Role,jwtInfo.UserType.ToString())

            };

            var expireTimes = DateTime.Now.AddMinutes(jwtInfo.ExpireMinutes);

            var tokenDescriptor = new JwtSecurityToken(jwtInfo.Issuer, jwtInfo.Audience,claims,null,expireTimes,credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
        
    }
}