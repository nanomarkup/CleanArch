using System;

namespace Services
{
    public static partial class Service
    {
        public static IServiceProvider Provider { get; set; }
        public static UserService User { get { return new UserService(Provider); } }
        public static MessageService Message { get { return new MessageService(Provider); } }    
    }
}
