using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Mapper;
using Core.Entities;
using Core.Interactors;
using Services;
using Entities;
using Interactors;

namespace ConsoleApp
{
    class Program : IServiceResponse<DtoIMessageId>
    {
        static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMessageEntity, MessageEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            services.AddTransient<IMessageInteractor, MessageInteractor>();
            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            CoreMapper.Initialize();
            Service.Provider = ConfigureServices();

            // Testing
            Service.Message.Send.Invoke(new DtoIMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 1."
            });

            Service.Message.Send.InvokeAsync(new DtoIMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 2."
            }, new Message());

            Service.Message.Send.InvokeAsync(new DtoIMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 3."
            }, new Program());

            Console.Read();
        }

        public void ServiceResponse(DtoIMessageId dto)
        {
            Console.WriteLine("The Main class notified about a message.");
        }

        class Message : IServiceResponse<DtoIMessageId>
        {
            public void ServiceResponse(DtoIMessageId dto)
            {
                Console.WriteLine("The Message class notified about a message.");
            }
        }
    }
}
