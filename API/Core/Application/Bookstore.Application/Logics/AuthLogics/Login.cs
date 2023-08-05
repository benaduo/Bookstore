using Bookstore.Application.Interfaces;
using Bookstore.Application.Logics.AuthLogics.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookstore.Application.Logics.AuthLogics
{
    public class Login : IRequest<object>
    {
        public LoginModel Model { get; set; }
    }

    public class LoginHandler : IRequestHandler<Login, object>
    {
        private IdentityUser _user;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        public LoginHandler(UserManager<IdentityUser> userManager, ILoggerManager logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<object> Handle(Login request, CancellationToken cancellationToken)
        {
            _user = await _userManager.FindByNameAsync(request.Model.Username);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, request.Model.Password));

            if (!result)
            {
                _logger.LogWarn($"{nameof(Login)}: Authentication failed. Wrong username or password");
            }
            var token = await CreateToken();
            return new { token };
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimsAsync()
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, _user.UserName)
            };

            return claims;

        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
