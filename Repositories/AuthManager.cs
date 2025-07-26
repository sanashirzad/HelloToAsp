using AutoMapper;
using HelloToAsp.Contracts;
using HelloToAsp.Data;
using HelloToAsp.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace HelloToAsp.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Login(LogUserDto logUserDto)
        {
            var user = await _userManager.FindByNameAsync(logUserDto.PhoneNumber);
            bool isValidUser = await _userManager.CheckPasswordAsync(user, logUserDto.Password);

            if (!isValidUser || user == null)
            {
                return null;
            }

            var token = await GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(RegUserDto regUserDto)
        {
            var user = _mapper.Map<User>(regUserDto);

            user.UserName = regUserDto.PhoneNumber;

            var result = await _userManager.CreateAsync(user, regUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return result.Errors;
        }

        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // this is the person whom the token has been issued
                // sub = subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
                new Claim("Id", user.Id.ToString()), // not necessary recommended
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
