using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }
}
