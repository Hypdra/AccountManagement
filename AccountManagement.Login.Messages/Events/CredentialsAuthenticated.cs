using AccountManagement.Framework;
using AccountManagement.Login.Messages.Enums;

namespace AccountManagement.Login.Messages.Events
{
    public class CredentialsAuthenticated : IEvent
    {
        public CredentialsAuthenticated(AuthenticationResult result)
        {
            Result = result;
        }

        public AuthenticationResult Result { get; private set; }
    }
}
