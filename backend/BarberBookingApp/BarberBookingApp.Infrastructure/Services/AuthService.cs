using BarberBookingApp.Application.DTOs.Authentication;
using BarberBookingApp.Application.Interfaces;
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



namespace BarberBookingApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Login(LoginDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new Exception($"User with email {request.Email} not found.");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
                throw new Exception("Invalid password");

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateToken(user, roles.FirstOrDefault() ?? "Customer");

            return new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "Customer",
                Token = token
            };
        }

        public async Task<AuthResponseDto> Register(RegisterDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception($"User with email {request.Email} already exists.");

            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            // Assign role
            await _userManager.AddToRoleAsync(user, request.Role);

            // TODO: Create Customer or Barber entity in our domain model

            // Generate JWT token
            var token = GenerateToken(user, request.Role);

            return new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = request.Role,
                Token = token
            };
        }

        private string GenerateToken(IdentityUser user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
