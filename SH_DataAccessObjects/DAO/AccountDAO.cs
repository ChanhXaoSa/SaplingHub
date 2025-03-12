using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Common.Model.Account;
using SH_BusinessObjects.Identity.Interface;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SH_DataAccessObjects.DAO.Interfaces;

namespace SH_DataAccessObjects.DAO
{
    public class AccountDAO(IIdentityService identityService, IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
        //SignInManager<ApplicationUser> signInManager, 
        IApplicationDbContext context) : IAccountDAO
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IConfiguration _configuration = configuration;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IApplicationDbContext _context = context;

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser
            {
                UserName = signUpModel.Email,
                Email = signUpModel.Email,
                LockoutEnabled = false,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }

        public async Task<object> SignInAsync(SignInModel signInModel)
        {
            var user = await _identityService.GetUserByEmailAsync(signInModel.Email);

            var passwordCheck = await _userManager.CheckPasswordAsync(user, signInModel.Password);

            if (user == null || !passwordCheck)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            var authClaim = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }
            AccountModel accountModel = new()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                IsAdmin = userRole.Contains("Admin"),
                IsUser = userRole.Contains("User") || !userRole.Contains("Admin")
            };
            IdentityModelEventSource.ShowPII = true;

            var key = _configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration.");
            if (key.Length < 16) throw new InvalidOperationException("JWT Key must be at least 16 characters long.");
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: authClaim,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                accountInfo = accountModel
            };
        }

        public async Task<ApplicationUser> GetAccountByIdAsync(string id)
        {
            return await Task.Run(() => _context.GetUser<ApplicationUser>().FirstOrDefault(ApplicationUser => ApplicationUser.Id == id)!);
        }

        public async Task<List<ApplicationUser>> GetAdminAcocuntAsync()
        {
            List<ApplicationUser> users = [.. _context.GetUser<ApplicationUser>()];
            List<ApplicationUser> adminUsers = [];
            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                foreach (var role in userRole)
                {
                    if (role == "Admin")
                    {
                        adminUsers.Add(user);
                    }
                }
            }
            return adminUsers;
        }

        public async Task<List<ApplicationUser>> GetUserAccountAsync()
        {
            List<ApplicationUser> users = [.. _context.GetUser<ApplicationUser>()];
            List<ApplicationUser> userUsers = [];
            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                foreach (var role in userRole)
                {
                    if (role == "User")
                    {
                        userUsers.Add(user);
                    }
                }
            }
            return userUsers;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await Task.Run(() => _context.GetUser<ApplicationUser>().ToList());
        }

        public async Task<bool> LockoutEnableAccount(string email)
        {
            var user = await _identityService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.Now.AddYears(100);
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> LockoutDisableAccount(string email)
        {
            var user = await _identityService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnabled = false;
            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}
