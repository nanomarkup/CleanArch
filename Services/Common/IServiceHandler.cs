using System.Threading.Tasks;

namespace Services
{
    public interface IServiceHandler<Request, Response>        
    {
        Response Handle(Request request);
        Task<Response> HandleAsync(Request request);
        void Handle(Request request, IServiceResponse<Response> service);
        Task HandleAsync(Request request, IServiceResponse<Response> service);        
    }
}
