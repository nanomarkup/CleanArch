using Core.Models;
using System;

namespace Core.Gateways
{
    public interface IMessageGateway : IPersistenceGateway<MessageModel, DtoMessageGatewayModify, DtoMessageGatewayQuery>
    { }

    public class DtoMessageGatewayModify
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageGatewayQuery
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
