using Loader;
using Services;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program : IServiceResponse<DtoServiceMessageId>
    {
        static void Initialize()
        {
            MapperInitializer.Initialize();
            DependencyInitializer.Initialize();            
        }

        static void Main(string[] args)
        {
            Initialize();

            var sender = Guid.NewGuid();
            var receiver = Guid.NewGuid();            

            // Send a "New message 1" text
            var messageId = Service.Message.Send.Invoke(new DtoServiceMessageSend()
            {
                Sender = sender,
                Receiver = receiver,
                Text = "New message 1."
            }).Id;

            // Read a message by id and print it
            Console.WriteLine(Service.Message.ReadById.Invoke(new DtoServiceMessageReadById() { Id = messageId }));

            // Read all messages for sender and receiver
            var messages = Service.Message.Read.Invoke(new DtoServiceMessageRead() { Sender = sender, Receiver = receiver });
            // Print number of messages
            Console.WriteLine($"Number of messages is {messages.Count()}.");
            // Print the first message
            Console.WriteLine($"The first message is '{messages.First()}'");

            // Send the next 2 messages with using callback function/interface
            Service.Message.Send.Invoke(new DtoServiceMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 2."
            }, new Message());

            Service.Message.Send.InvokeAsync(new DtoServiceMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "New message 3."
            }, new Program());

            Console.Read();
        }

        public void ServiceResponse(DtoServiceMessageId dto)
        {
            Console.WriteLine($"The Main class notified about a message {dto.Id}.");
        }

        class Message : IServiceResponse<DtoServiceMessageId>
        {
            public void ServiceResponse(DtoServiceMessageId dto)
            {
                Console.WriteLine($"The Message class notified about a message {dto.Id}.");
            }
        }
    }
}
