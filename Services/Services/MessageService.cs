using System;
using System.Collections.Generic;
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

        public IServiceHandler<DtoMessageSendInteractor, DtoMessageIdInteractor> Send
        {
            get
            {
                return new BaseService<DtoMessageSendInteractor, DtoMessageIdInteractor>(x => Provider.GetService<IMessageInteractor>().Send(x));
            }
        }

        public IServiceHandler<DtoMessageReadInteractor, IEnumerable<DtoMessageInfoInteractor>> Read
        {
            get
            {
                return new BaseService<DtoMessageReadInteractor, IEnumerable<DtoMessageInfoInteractor>>(x => Provider.GetService<IMessageInteractor>().Read(x));
            }
        }

        public IServiceHandler<DtoMessageReadByIdInteractor, DtoMessageInfoInteractor> ReadById
        {
            get
            {
                return new BaseService<DtoMessageReadByIdInteractor, DtoMessageInfoInteractor>(x => Provider.GetService<IMessageInteractor>().Read(x));
            }
        }

        public IServiceHandler<DtoMessageReadByDateInteractor, IEnumerable<DtoMessageInfoInteractor>> ReadByDate
        {
            get
            {
                return new BaseService<DtoMessageReadByDateInteractor, IEnumerable<DtoMessageInfoInteractor>>(x => Provider.GetService<IMessageInteractor>().Read(x));
            }
        }  
    }
}
