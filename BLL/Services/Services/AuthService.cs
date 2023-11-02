using BLL.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Renty.BLL.Helper;
using DAL.Entities;
using Renty.Models;
using Renty.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace BLL.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> UserManager, IOptions<JWT> jwt)
        {
            _userManager = UserManager;
            _jwt = jwt.Value;
        }

        #region Authentication Services
        public async Task<Response<AuthModel>> LoginAsync(LoginUser login)
        {
            try
            {
                // Check if email or password true or not
                var user = new EmailAddressAttribute().IsValid(login.Email) ? _userManager.FindByEmailAsync(login.Email).Result : _userManager.FindByNameAsync(login.Email).Result;

                if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                    return new() { Message = "Invalid password or email" };


                // create token for the user
                var jwtSecurityToken = await CreateJwtToken(user);
                var studentRoles = await _userManager.GetRolesAsync(user);


                return new Response<AuthModel>
                {
                    Success = true,
                    ObjectData = new AuthModel()
                    {
                        Email = user.Email,
                        ExpiresOn = jwtSecurityToken.ValidTo,
                        IsAuthenticated = true,
                        Roles = studentRoles.ToList(),
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Username = user.UserName,
                        
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response<AuthModel>
                {
                    Success = false,
                    status_code = "404",
                    Message = "Not Found"
                };
            }
        }

        public async Task<Response<AuthModel>> RegisterUserAsync(RegisterModel model)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(model.Email) is not null)
                    return new Response<AuthModel> { Message = "Email is already registered!" };

                if (await _userManager.FindByNameAsync(model.Username) is not null)
                    return new Response<AuthModel> { Message = "Username is already registered!" };

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FullName = model.FullName,
                    IsActive = true,
                    NationalId = model.NationalId,
                    Nationality = model.Nationality,
                    PhoneNumber =model.PhoneNumber,
                    Gender = model.Gender,
                    BirithDate=model.BirithDate,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in result.Errors)
                        errors += $"{error.Description},";

                    return new Response<AuthModel> { Message = errors };
                }

                await _userManager.AddToRoleAsync(user, Roles.User.ToString());


                var jwtSecurityToken = await CreateJwtToken(user);


                await _userManager.UpdateAsync(user);
                var studentRoles = await _userManager.GetRolesAsync(user);
                AuthModel objectData = new AuthModel()
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = studentRoles.ToList(),
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Username = user.UserName
                };

                return new Response<AuthModel>
                {
                    Success = true,
                    ObjectData = objectData
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
                return new() { Message = ex.Message };
            }

        }
        #endregion

        #region Private Methods
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        #endregion
    }
}
