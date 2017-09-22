using System;

namespace Services
{
    public static partial class Service
    {
        public static IServiceProvider Provider { get; set; }
        public static MessageService Message { get { return new MessageService(Provider); } }
    }
}
