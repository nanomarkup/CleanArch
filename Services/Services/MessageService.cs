using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Interactors;

namespace Services
{
    public class MessageService
    {
        IServiceProvider Provider { get; }

        public MessageService(IServiceProvider provider)
        {
            Provider = provider;
        }          

        public IServiceHandler<DtoIMessageSendRequest, DtoIMessageSendResponse> Send
        {
            get
            {
                return new BaseService<DtoIMessageSendRequest, DtoIMessageSendResponse>(x =>
                {
                    return Provider.GetService<IMessageInteractor>().Send(x);
                });
            }
        }
    }
}
