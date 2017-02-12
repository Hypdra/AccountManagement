using System.Threading.Tasks;
using AccountManagement.Login.Messages.Commands;
using MassTransit;
using AccountManagement.Login.Messages.Events;
using AccountManagement.Login.Messages.Enums;

namespace AccountManagement.Login
{
    public class AuthenticateCredentialsConsumer : IConsumer<AuthenticateCredentials>
    {
        public async Task Consume(ConsumeContext<AuthenticateCredentials> context)
        {
            var result = context.Message.Password == "password" ? AuthenticationResult.Accepted : AuthenticationResult.Rejected;
            context.Respond(new CredentialsAuthenticated(result));
        }
    }
}
