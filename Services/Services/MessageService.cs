using AutoMapper;
using Core.Interactors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Services
{
    public class MessageService
    {
        IServiceProvider Provider { get; }

        public MessageService(IServiceProvider provider)
        {
            Provider = provider;
        }          

        public IServiceHandler<DtoServiceMessageSend, DtoServiceMessageId> Send
        {
            get
            {
                return new BaseService<DtoServiceMessageSend, DtoServiceMessageId>(x => 
                    Mapper.Map<DtoServiceMessageId>(Provider.GetService<IMessageInteractor>().Send(Mapper.Map<DtoMessageSendInteractor>(x))));
            }
        }

        public IServiceHandler<DtoServiceMessageRead, IEnumerable<DtoServiceMessageInfo>> Read
        {
            get
            {
                return new BaseService<DtoServiceMessageRead, IEnumerable<DtoServiceMessageInfo>>(x => 
                    Mapper.Map<IEnumerable<DtoServiceMessageInfo>>(Provider.GetService<IMessageInteractor>().Read(Mapper.Map<DtoMessageReadInteractor>(x))));
            }
        }

        public IServiceHandler<DtoServiceMessageReadById, DtoServiceMessageInfo> ReadById
        {
            get
            {
                return new BaseService<DtoServiceMessageReadById, DtoServiceMessageInfo>(x => 
                    Mapper.Map<DtoServiceMessageInfo>(Provider.GetService<IMessageInteractor>().Read(Mapper.Map<DtoMessageReadByIdInteractor>(x))));
            }
        }

        public IServiceHandler<DtoServiceMessageReadByDate, IEnumerable<DtoServiceMessageInfo>> ReadByDate
        {
            get
            {
                return new BaseService<DtoServiceMessageReadByDate, IEnumerable<DtoServiceMessageInfo>>(x => 
                    Mapper.Map<IEnumerable<DtoServiceMessageInfo>>(Provider.GetService<IMessageInteractor>().Read(Mapper.Map<DtoMessageReadByDateInteractor>(x))));
            }
        }  
    }

    public class DtoServiceMessageId
    {
        public Guid Id { get; set; }
    }

    public class DtoServiceMessageInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Text}";
        }
    }

    public class DtoServiceMessageSend
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoServiceMessageRead
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoServiceMessageReadById
    {
        public Guid Id { get; set; }
    }

    public class DtoServiceMessageReadByDate
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
