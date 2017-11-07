using Core.Models;
using System;

namespace Core.Gateways
{
    public interface IMessageGateway : IPersistenceGateway<MessageModel, DtoMessageModifiedGateway, DtoMessageQueryGateway>
    { }

    public class DtoMessageModifiedGateway
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageQueryGateway
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
