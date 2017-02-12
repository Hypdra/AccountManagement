using AccountManagement.Login.Messages.Commands;
using AccountManagement.Login.Messages.Events;
using Autofac;
using MassTransit;
using System;

namespace AccountManagement.WebApi
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var busControl = BusConfig.BusControl;
            builder.Register(_ => busControl).SingleInstance()
                                                       .As<IBusControl>()
                                                       .As<IBus>();
            var authenticateServiceAddress = new Uri("rabbitmq://localhost/login");
            var client = busControl.CreateRequestClient<AuthenticateCredentials, CredentialsAuthenticated>(authenticateServiceAddress, TimeSpan.FromSeconds(10));
            builder.Register(_ => client).As<IRequestClient<AuthenticateCredentials, CredentialsAuthenticated>>();
        }
    }
}