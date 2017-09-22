using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interactors;
using AutoMapper;

namespace Interactors
{
    public class MessageInteractor : BaseInteractor, IMessageInteractor
    {
        public MessageInteractor(IServiceProvider provider) : base(provider) { }

        public DtoIMessageSendResponse Send(DtoIMessageSendRequest dto)
        {
            var message = GetService<IMessageEntity>();
            message.Create(Mapper.Map<DtoEMessage>(dto));
            return new DtoIMessageSendResponse()
            {
                Result = message.Send()
            };
        }

        public IEnumerable<string> Read(DtoIMessageReadRequest dto)
        {
            return Read(Mapper.Map<DtoIMessageReadByDateRequest>(dto));
        }

        public IEnumerable<string> Read(DtoIMessageReadByDateRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}
