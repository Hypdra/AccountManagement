using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using AccountManagement.Login.Messages.Events;
using AccountManagement.Login.Messages.Commands;
using MassTransit;
using System;
using AccountManagement.Login.Messages.Enums;

namespace AccountManagement.WebApi.Providers
{
    public class AccountManagementAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private readonly IRequestClient<AuthenticateCredentials, CredentialsAuthenticated> _authenticationClient;

        public AccountManagementAuthorizationProvider(IRequestClient<AuthenticateCredentials, CredentialsAuthenticated> authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var authenticationResult = await _authenticationClient.Request(new AuthenticateCredentials(context.Password, "Email@email.tt", DateTime.Now));

            if(authenticationResult.Result == AuthenticationResult.Rejected)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}