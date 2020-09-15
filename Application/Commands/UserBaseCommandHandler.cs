using System;
using Microsoft.Extensions.Logging;

namespace AuthorizationAPI.Application.Commands
{
    public class UserBaseCommandHandler
    {
        protected readonly IUserService _userService;
        protected readonly ILogger _logger;

        protected UserBaseCommandHandler(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger("UserAuthorization");
        }

        protected Exception LogErrorAndThrowException(string exceptionMessage, Exception exception = null)
        {
            // TODO User LogLevel
            _logger.LogError(exceptionMessage);
            throw new Exception(exceptionMessage);
        }
    }
}