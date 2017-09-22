using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IMessageGateway : IPersistenceGateway<DtoGMessageInfo, DtoGMessageModified, DtoGMessageQuery>
    { }

    public class DtoGMessageInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoGMessageModified
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string Text { get; set; }
    }

    public class DtoGMessageQuery
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
