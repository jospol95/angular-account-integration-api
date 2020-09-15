using System;
using System.ComponentModel.DataAnnotations;
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
    public class RegisterUserCommand : IRequest<UserDto>
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(24)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(60)]
        public string LastName { get; set; }
        
        public AuthenticationTypeEnum? AuthenticationType { get; set; }
        
        public class RegisterUserCommandHandler: UserBaseCommandHandler, IRequestHandler<RegisterUserCommand, UserDto>
        {
            public RegisterUserCommandHandler(IUserService userService, ILoggerFactory logger) : 
                base(userService,logger)
            {
                
            }
            
            public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                request.AuthenticationType ??= AuthenticationTypeEnum.AuthorizationApi;
                
                var existingUser = await _userService.GetUserByEmailAsync(request.Email) ;
                if (existingUser != null) throw new EmailAlreadyRegisteredException("This email is already registered.");
                
                try
                {
                    await _userService.SaveUserAsync(request.Email, request.Password, request.FirstName,
                        request.LastName, (int) request.AuthenticationType);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured when trying to create user {email}", request.Email, ex);
                    throw;

                }

                var addedUser = await _userService.GetUserByEmailAsync(request.Email);

                var userDto = new UserDto()
                {
                    Email = addedUser.Email,
                    FirstName = addedUser.FirstName,
                    Id = addedUser.Id,
                    LastName = addedUser.LastName
                };
                return userDto;

            }

            
        }
        
    }
}