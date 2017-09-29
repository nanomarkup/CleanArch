using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;
using Core.Interactors;
using Core.Gateways;
using Services;
using Entities;
using Interactors;
using Infrastructure.Context;
using Infrastructure.Gateways;

namespace ConsoleApp
{
    class Program : IServiceResponse<DtoMessageIdInteractor>
    {
        static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new Core.Common.EntityProfile());
                cfg.AddProfile(new Core.Common.GatewayProfile());
                cfg.AddProfile(new Core.Common.InteractorProfile());
                cfg.AddProfile(new Infrastructure.Common.EntityProfile());
                cfg.AddProfile(new Infrastructure.Common.GatewayProfile());
            });

            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<InfrastructureDbContext>(opt => opt.UseInMemoryDatabase("example"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            services.AddTransient<IMessageEntity, MessageEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            services.AddTransient<IMessageInteractor, MessageInteractor>();            
            services.AddTransient<IMessageGateway, MessageGateway>();
            services.AddTransient<IInfrastructureDbContext, InfrastructureDbContext>();
            Service.Provider = services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Initialize();

            var sender = Guid.NewGuid();
            var receiver = Guid.NewGuid();            

            // Send a "New message 1" text
            var messageId = Service.Message.Send.Invoke(new DtoMessageSendInteractor()
            {
                Sender = sender,
                Receiver = receiver,
                Text = "New message 1."
            }).Id;

            // Read a message by id and print it
            Console.WriteLine(Service.Message.ReadById.Invoke(new DtoMessageReadByIdInteractor() { Id = messageId }));

            // Read all messages for sender and receiver
            var messages = Service.Message.Read.Invoke(new DtoMessageReadInteractor() { Sender = sender, Receiver = receiver });
            // Print number of messages
            Console.WriteLine($"Number of messages is {messages.Count()}.");
            // Print the first message
            Console.WriteLine($"The first message is '{messages.First()}'");

            // Send the next 2 messages with using callback function/interface
            Service.Message.Send.Invoke(new DtoMessageSendInteractor()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 2."
            }, new Message());

            Service.Message.Send.InvokeAsync(new DtoMessageSendInteractor()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 3."
            }, new Program());

            Console.Read();
        }

        public void ServiceResponse(DtoMessageIdInteractor dto)
        {
            Console.WriteLine($"The Main class notified about a message {dto.Id}.");
        }

        class Message : IServiceResponse<DtoMessageIdInteractor>
        {
            public void ServiceResponse(DtoMessageIdInteractor dto)
            {
                Console.WriteLine($"The Message class notified about a message {dto.Id}.");
            }
        }
    }
}
