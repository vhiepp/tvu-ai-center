﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AICenterAPI.Datas;
using AICenterAPI.Models;
using AICenterAPI.Repositories;

namespace AICenterAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<object> RegisterCustomer(SignUpModel signUp)
        {
            var errors = new Dictionary<string, string>();

            if (signUp.Email != null)
            {
                var email = await _userRepository.FindByEmail(signUp.Email);
                if (email != null)
                {
                    errors.Add("Email", "Email already exists");
                }
            }
            if (signUp.PhoneNumber != null) {
                var phone = await _userRepository.FindByPhone(signUp.PhoneNumber);
                if (phone != null)
                {
                    errors.Add("PhoneNumber", "Phone number already exists");
                }
            }
            if (errors.Count > 0) return await Task.FromResult(errors);
            User user = new User
            {
                Password = HashPassword(signUp.Password),
                PhoneNumber = signUp.PhoneNumber,
                UserName = signUp.PhoneNumber + "",
                Email = signUp.Email,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                WardId = signUp.WardId,
                DistrictId = signUp.DistrictId,
                ProvinceId = signUp.ProvinceId,
                Address = signUp.Address,
                Gender = signUp.Gender,
                IsCustomer = true,
                IsSuperAdmin = false,
                RoleId = null,
            };
            await _userRepository.AddAsync(user);
            return await Task.FromResult(true);
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JWT");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
            };

            if (user.IsSuperAdmin)
            {
                claims.Add(new Claim("Permission", RoleDefaultTypes.SuperAdmin.KeyName));
            }
            else
            if (user.IsCustomer)
            {
                claims.Add(new Claim("Permission", RoleDefaultTypes.Customer.KeyName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] + ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["Expires"] + "")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.VerifyHashedPassword(null, hashedPassword, password) == PasswordVerificationResult.Success;
        }

        public async Task<object> Login(SignInModel signIn)
        {
            var user = await _userRepository.FindByUserName(signIn.UserName);
            //Console.WriteLine(user);
            if (user == null)
            {
                return await Task.FromResult(new Dictionary<string, string> { { "UserName", "User not found" } });
            }
            if (!VerifyPassword(signIn.Password, user.Password))
            {
                return await Task.FromResult(new Dictionary<string, string> { { "Password", "Password is incorrect" } });
            }
            var profile = await GetProfile(user);
            return await Task.FromResult(new { AccessToken = GenerateJwtToken(user), Profile = profile });
        }

        public async Task<object> Profile(int userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return await Task.FromResult(new Dictionary<string, string> { { "User", "User not found" } });
            }
            return await GetProfile(user);
        }
    
        private async Task<ProfileModel> GetProfile(User user)
        {
            RoleModel? roleModel = null;
            if (user.IsSuperAdmin)
            {
                roleModel = new RoleModel
                {
                    Id = 0,
                    Name = RoleDefaultTypes.SuperAdmin.Name,
                    Description = RoleDefaultTypes.SuperAdmin.Description
                };
            }
            else
            if (user.IsCustomer)
            {
                roleModel = new RoleModel
                {
                    Id = 0,
                    Name = RoleDefaultTypes.Customer.Name,
                    Description = RoleDefaultTypes.Customer.Description
                };
            }
            else
            if (user.RoleId != null)
            {
                var role = await _roleRepository.FindByIdAsync(user.RoleId.Value);
                if (role != null)
                {
                    roleModel = new RoleModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.Description
                    };
                }
            }
            var profile = new ProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                WardId = user.WardId,
                DistrictId = user.DistrictId,
                ProvinceId = user.ProvinceId,
                Gender = user.Gender,
                IsSuperAdmin = user.IsSuperAdmin,
                IsCustomer = user.IsCustomer,
                RoleId = user.RoleId,
                Role = roleModel
            };
            return profile;
        }

    }
}
