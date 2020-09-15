using System.Threading.Tasks;
using AuthorizationAPI.Domain.Models;

namespace AuthorizationAPI.Application
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        void GetPasswordHashAndSalt(string password, out byte[] passwordHalt, out byte[] passwordSalt);

        Task<bool> ValidatePasswordHashAndSaltAsync(string password, byte[] userPasswordHash, byte[] userPasswordSalt);

        Task SaveUserAsync(string email, string password, string firstName, string lastName,
            int authenticationTypeId);
        
        
    }
}