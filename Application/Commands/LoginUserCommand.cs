using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationAPI.Application.DTOs;
using AuthorizationAPI.Application.Exceptions;
using AuthorizationAPI.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthorizationAPI.Application.Commands
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    public class LoginUserCommandCommandHandler : UserBaseCommandHandler, IRequestHandler<LoginUserCommand, UserDto>
    {
        
        public LoginUserCommandCommandHandler(IUserService userService, ILoggerFactory logger) : 
            base(userService, logger)
        {
        }
        
        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userService.GetUserByEmailAsync(request.Email);
            if(existingUser == null) throw new EmailAndOrPasswordIncorrectException();

            var passwordMatched = await _userService.ValidatePasswordHashAndSaltAsync(request.Password,
                existingUser.PasswordHash, existingUser.PasswordSalt);
            if (!passwordMatched) throw new EmailAndOrPasswordIncorrectException();
            
            return new UserDto();

        }

        
    }
}