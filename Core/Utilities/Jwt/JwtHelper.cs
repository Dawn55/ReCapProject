using Core.Concrete.Entities;
using Core.Encryption;
using Core.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Jwt
{
   public class JwtHelper : ITokenHelper
    {
        public IConfiguration _configuration { get; set; }
       public TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration = DateTime.Now.AddMinutes(20); 
        
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
           
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //_accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSingingCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials, user , operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            AccessToken ac = new AccessToken();
            ac.Token = token;
            ac.Expration = _accessTokenExpiration;
            return ac;
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,SigningCredentials signingcredentials,
            User user,List<OperationClaim> operationClaims)
        {
          var jwt = new JwtSecurityToken(
              issuer : tokenOptions.Issuer,
              audience : tokenOptions.Audience,
              expires : _accessTokenExpiration,
              notBefore : DateTime.Now,
              claims : SetClaims(user,operationClaims),
              signingCredentials : signingcredentials
              );
            return jwt;
        }
        private List<Claim> SetClaims(User user,List<OperationClaim> operationclaims)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddRoles(operationclaims.Select(p=>p.Name).ToArray());
            return claims;
        }
    }
}
