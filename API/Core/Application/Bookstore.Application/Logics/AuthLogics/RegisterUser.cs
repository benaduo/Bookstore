using Bookstore.Application.Logics.AuthLogics.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Application.Logics.AuthLogics
{
    public class RegisterUser : IRequest<IdentityResult>
    {
        public RegisterUserModel Model { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUser, IdentityResult>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterUserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Model.Username,
                Email = request.Model.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Model.Password);

            return result;
        }
    }
}
