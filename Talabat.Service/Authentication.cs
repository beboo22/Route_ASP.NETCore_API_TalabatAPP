using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;
using Talabat.Core.ServiceContract.Authentication;

namespace Talabat.Service
{
    public class Authentication : IAuthentication
    {
        public Authentication(IConfiguration configuration)
        {
            _Config = configuration;
        }

        public IConfiguration _Config { get; }

        public async Task<string> CreateTokenAsync(AppUser _user, UserManager<AppUser> _userManger)
        {
            //create privet Claims
            var Authclaims = new List<Claim>(){
                new Claim(ClaimTypes.Name,_user.DisplayName),
                new Claim(ClaimTypes.Email, _user.Email)
            };
            //Add Role
            var UserRole = await _userManger.GetRolesAsync(_user);
            foreach (var role in UserRole)
            {
                Authclaims.Add(new Claim(ClaimTypes.Role, role));
            }


            // create Security key

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Authkey"] ?? string.Empty));

            // create Token 


            var Token = new JwtSecurityToken
                (
                audience: _Config["Jwt:VaildAudience"],
                issuer: _Config["Jwt:validIssuer"],
                expires: DateTime.Now.AddDays(double.Parse(_Config["Jwt:validationDay"]??"0")),
                claims: Authclaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);


        }
    }
}
