using Microsoft.Extensions.DependencyInjection;
using Core.Interactors;

namespace Services
{
    public static partial class Service
    {      
        public static IServiceHandler<DtoMessageIntSendRequest, DtoMessageIntSendResponse> SendMessage
        {
            get
            {
                return new BaseService<DtoMessageIntSendRequest, DtoMessageIntSendResponse>(x =>
                {
                    return Provider.GetService<IMessageInteractor>().Send(x);
                });
            }
        }
    }
}
