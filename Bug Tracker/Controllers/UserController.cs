﻿using Bug_Tracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bug_Tracker.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _context = (ApplicationDbContext)configuration;
        }

        [HttpPost("RegisterUser")]


        public IActionResult RegisterUser([FromBody] User user)
        {

            User person = _context.Users.FirstOrDefault(f => f.Role == UserRole.RegularUser && f.Email == user.UserName);

            if (person == null)
            {
                var newuser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    HashedPassword = user.HashedPassword,
                    Role = user.Role,
                    UserId = user.UserId,

                };
                _context.Users.Add(newuser);
                _context.SaveChanges();

                return Ok("Registartion completed ");
            }
            else
            {
                return BadRequest("User already exist");
            }

        }
        [HttpGet("GetUser")]
        public IActionResult GetUser(string UserName, string password)
        {
            var user = _context.Users.FirstOrDefault(f => f.UserName == UserName && f.HashedPassword == password);

            if (user is not null)
            {
                var res = new User
                {
                    UserName = user.UserName,
                    HashedPassword = user.HashedPassword,
                    Email = user.Email,
                };

                return Ok(user);
            }
            else
            {
                return BadRequest(" User not Found");

            }
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == credentials.Email && u.HashedPassword == credentials.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });

        }

        public string GenerateJwtToken(User user)
        {


            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt: Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );  

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
