using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiCleanArch.Application.ViewModels.AppSettingViewModels;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;
using WebApiCleanArch.Domain.Interfaces.Services.JwtServices;

namespace WebApiCleanArch.Infrastructure.Services.JwtServices
{
    public class JwtService : IJwtService, ITransientDependency
    {

        #region Fields

        private readonly JwtSetting _jwtSetting;
        private readonly SignInManager<User> signInManager;
        #endregion

        #region Constructors

        public JwtService(IOptions<JwtSetting> options, SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
            _jwtSetting = options.Value;
        }

        #endregion

        #region Methods

        public string Generate(User user)
        {


            var secretKey = Encoding.UTF8.GetBytes(_jwtSetting.SecreteKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);


            var encryptionKey = Encoding.UTF8.GetBytes(_jwtSetting.EncryptKey);
            var encryptionCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            //claims should get from IUserRoleService
            var claims = GetClaims(user);


            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                EncryptingCredentials = encryptionCredential,
                SigningCredentials = signingCredentials,
                Audience = _jwtSetting.Audience,
                Expires = DateTime.Now.AddMinutes(_jwtSetting.ExpirationMiniutes),
                NotBefore = DateTime.Now.AddMinutes(_jwtSetting.NotBeforMiniutes),
                IssuedAt = DateTime.Now,
                Issuer = _jwtSetting.Issuer,
                Subject = new ClaimsIdentity(claims.Result),
                CompressionAlgorithm = CompressionAlgorithms.Deflate,
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
            var jwt = tokenHandler.WriteToken(jwtSecurityToken);

            return jwt;


        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            //            var claims = new List<Claim>
            //           {
            //               new Claim(ClaimTypes.Name,user.Name),
            //               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            //           };

            var claims = await signInManager.ClaimsFactory.CreateAsync(user);
            //add custom claims
            var list = new List<Claim>(claims.Claims);
            //add user roles
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin",

                }
            };

            list.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return list;
        }

        #endregion


    }
}
