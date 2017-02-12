using Autofac;

namespace AccountManagement.Login
{
    public class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticateCredentialsConsumer>();
        }
    }
}
