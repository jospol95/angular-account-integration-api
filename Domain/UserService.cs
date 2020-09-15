using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Application;
using AuthorizationAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace AuthorizationAPI.Domain
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _authDbContext;

        public UserService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        
        public async Task<User> GetUserByIdAsync(string id)
        {
            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return existingUser;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var existingUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return existingUser;
        }
        
        public async Task SaveUserAsync(string email, string password, string firstName,string lastName, 
            int authenticationTypeId)
        {
            GetPasswordHashAndSalt(password, out var passwordHash, out var passwordSalt);
            var user = new User( Guid.NewGuid().ToString(), email, passwordHash, passwordSalt, firstName, lastName, authenticationTypeId);

            await _authDbContext.Users.AddAsync(user);
            await _authDbContext.SaveChangesAsync();
        }

        public void GetPasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<bool> ValidatePasswordHashAndSaltAsync(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt);
            var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if(computerHash.Where((value, count) => value != userPasswordHash[count]).Any())
            {
                return false;
            }

            return true;
        }

        
        
    }
}