using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyHub.Data;
using StudyHub.Entities;
using StudyHub.Model;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyHub.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext context;
        private readonly IRSAHelperService rsaservice;

        public AuthService(IConfiguration configuration, MyDbContext context, IRSAHelperService rsaservice)
        {
            _configuration = configuration;
            this.context = context;
            this.rsaservice = rsaservice;
        }


        public async Task<User?> RegisterAsync(UserDto request)
        {
            if (await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }
            var user = new User();
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.Name = request.Name;
            user.Email = request.Email;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;

        }
        public async Task<List<User>> GetUserAsync()
        {
            var result= await context.Users.ToListAsync();
            return result;
        }
         
        public async Task<string?> LoginAsync(UserDto request)
        {
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return null;
            }
            var decPass = this.rsaservice.Decrypt(request.Password);

            if (!BCrypt.Net.BCrypt.Verify(decPass, user.PasswordHash))
            {
                return null;
            }
            string token = CreateToken(user);

            return token;

        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
       public async Task<UserDetailsDto> GetUserDetails(string userId)
        {
           
            int parsedId = int.Parse(userId);
            var userData = context.Users.Find(parsedId);
            UserDetailsDto userDetailsObj = new UserDetailsDto()
            {
               Name=userData.Name,
               Username=userData.Username,
               Email =userData.Email,
               Education=userData.Education,
               Country=userData.Country,
               Address=userData.Address,
                Phone = userData.Phone,
                AboutInfo =userData.AboutInfo
               
            };
            return userDetailsObj;
        }
        public async Task<User> UpdateUserDetails(string userId, UserDetailsDto updateDetails)
        {
            int parsedId = int.Parse(userId);
            var userData = context.Users.Find( parsedId);
            userData.Country = updateDetails.Country;
            userData.Address = updateDetails.Address;
            userData.Phone = updateDetails.Phone;
            userData.AboutInfo = updateDetails.AboutInfo;
            userData.Email = updateDetails.Email;
            userData.Name = updateDetails.Name;
            userData.Education = updateDetails.Education;
             
            await context.SaveChangesAsync();
            return userData;

        }
        public async Task<UserIdDto> GetUserId(string userId)
        {
            var parsedId = int.Parse(userId);
            UserIdDto userData = new UserIdDto()
            {
                User_Id = parsedId,
            };
            return userData;
        }


    }
}

