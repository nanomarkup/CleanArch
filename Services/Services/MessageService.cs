using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;
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

        public IServiceHandler<DtoIMessageSend, DtoGuidResponse> Send
        {
            get
            {
                return new BaseService<DtoIMessageSend, DtoGuidResponse>(x =>
                {
                    return new DtoGuidResponse()
                    {
                        Result = Provider.GetService<IMessageInteractor>().Send(x)
                    };
                });
            }
        }

        public IServiceHandler<DtoIMessageRead, IEnumerable<DtoIMessageInfo>> Read
        {
            get
            {
                return new BaseService<DtoIMessageRead, IEnumerable<DtoIMessageInfo>>(x =>
                {
                    return Provider.GetService<IMessageInteractor>().Read(x);
                });
            }
        }

        public IServiceHandler<DtoIMessageReadByDate, IEnumerable<DtoIMessageInfo>> ReadByDate
        {
            get
            {
                return new BaseService<DtoIMessageReadByDate, IEnumerable<DtoIMessageInfo>>(x =>
                {
                    return Provider.GetService<IMessageInteractor>().Read(x);
                });
            }
        }  
    }
}
