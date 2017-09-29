using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class MessageEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }
}
