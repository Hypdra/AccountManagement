using AccountManagement.Login;
using MassTransit;
using System;

namespace AccountManagement.WebApi
{
    public class BusConfig
    {
        public static IBusControl BusControl { get; set; }

        public static void ConfigureBus()
        {
            BusControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "login", e => { e.Consumer<AuthenticateCredentialsConsumer>(); });
            });

            BusControl.Start();
        }

    }
}