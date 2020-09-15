using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizationAPI.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int AuthenticationTypeId { get; set; }
        public AuthenticationType AuthenticationType { get; set; }

        public User(string id, string email, byte[] passwordHash, byte[] passwordSalt, string firstName,
            string lastName, int authenticationTypeId)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            FirstName = firstName;
            LastName = lastName;
            AuthenticationTypeId = authenticationTypeId;
        }
        
        // public User(string email, byte[] passwordHash, byte[] passwordSalt, string firstName,
        //     string lastName, int authenticationTypeId)
        // {
        //     Id = new Guid().ToString();
        //     Email = email;
        //     PasswordHash = passwordHash;
        //     PasswordSalt = passwordSalt;
        //     FirstName = FirstName;
        //     LastName = LastName;
        //     AuthenticationTypeId = authenticationTypeId;
        // }
    }
}