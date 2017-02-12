using AccountManagement.Framework;
using System;

namespace AccountManagement.Login.Messages.Commands
{
    public class AuthenticateCredentials : ICommand
    {
        public AuthenticateCredentials(string password, string email, DateTime dateOfBirth)
        {
            Password = password;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public string Password { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }
    }
}
