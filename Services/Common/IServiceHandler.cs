using System.Threading.Tasks;

namespace Services
{
    public interface IServiceHandler<Request, Response>        
    {
        Response Invoke(Request request);
        Task<Response> InvokeAsync(Request request);
        void Invoke(Request request, IServiceResponse<Response> service);
        Task InvokeAsync(Request request, IServiceResponse<Response> service);        
    }
}
