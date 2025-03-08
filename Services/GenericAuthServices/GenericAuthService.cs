//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.GenericAuthServices
//{
//    public class GenericAuthService<T> : IGenericAuthService<T> where T : class
//    {
//        private readonly IConfiguration  config;

//        public GenericAuthService(IConfiguration config)
//        {
//            this.config = config;
//        }

//        public async Task<string> AuthenticateAsync(T obj)
//        //{
//            var jwtSetting = config.GetSection("Jwt");
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                 new Claim(JwtRegisteredClaimNames.Sub, obj.name),
//                 new Claim("UserType", typeof(T).Name),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var token = new JwtSecurityToken(
//                jwtSetting["Issuer"],
//                jwtSetting["Audience"],
//                claims,
//                expires: DateTime.UtcNow.AddHours(1),
//                signingCredentials: creds
//                );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
