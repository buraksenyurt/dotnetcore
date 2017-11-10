using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HelloJWT
{
    [Route("token")]
    [AllowAnonymous()]
    public class TokenController
        :Controller
        {
            [HttpPost("new")]
            public IActionResult GetToken([FromBody]UserInfo user)
            {
                Console.WriteLine("User name:{0}",user.Username);
                Console.WriteLine("Password:{0}",user.Password);

                if(IsValidUserAndPassword(user.Username,user.Password))
                    return new ObjectResult(GenerateToken(user.Username));
                    
                return Unauthorized();
            }

        private string GenerateToken(string userName)
        {
            var someClaims=new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                new Claim(JwtRegisteredClaimNames.Email,"heimdall@mail.com"),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };

            SecurityKey securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"));
            var token=new JwtSecurityToken(
                issuer:"west-world.fabrikam.com",
                audience:"heimdall.fabrikam.com",                
                claims:someClaims,       
                expires:DateTime.Now.AddMinutes(3),                
                signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsValidUserAndPassword(string userName, string password)
        {
            //Sürekli true dönüyor. Normalde bir Identity mekanizması ile entegre etmemiz lazım.
            return true;
        }
    }
}