using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IMessageGateway : IPersistenceGateway<DtoMessageInfoGateway, DtoMessageModifiedGateway, DtoMessageQueryGateway>
    { }

    public class DtoMessageInfoGateway
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

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
