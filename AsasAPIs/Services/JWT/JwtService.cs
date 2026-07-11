using Asas.AsasHash;
using AsasAPIs.Data;
using AsasAPIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace AsasAPIs.Services.JWT
{
    public class JwtService
    {
        private readonly AsasContext _asasContext;
        private readonly IConfiguration _configuration;
        public JwtService(AsasContext asasContext, IConfiguration configuration)
        {
            _asasContext = asasContext;
            _configuration = configuration;

        }

        public async Task<LoginRes> Authenticate(LoginReq loginReq)
        {
            if (string.IsNullOrEmpty(loginReq.EmpId) || string.IsNullOrEmpty(loginReq.Password))
            {
                return null;
            }
            //Using a Hashing Service to verify the password
            var UserAccount = await _asasContext.AddEmp.FirstOrDefaultAsync(e => e.EmpId == loginReq.EmpId);
            if(UserAccount is null || !AsasHashPassword.VerifyPassword(loginReq.Password, UserAccount.Password))
            {
                return null;
            }
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMinutes = _configuration.GetValue<int>("JwtConfig:TokenExpirationInMinutes");
            var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenValidityMinutes);


            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim( JwtRegisteredClaimNames.Sub , UserAccount.EmpId)
                   
                }),
                Expires = tokenExpiration,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            var jwtToken = tokenHandler.WriteToken(token);

            return new LoginRes
            {
                EmpId = UserAccount.EmpId,
                AccessToken = jwtToken,
                ExpiresInSeconds = tokenValidityMinutes * 60
            };

        }

    }
}

    
