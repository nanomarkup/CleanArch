using System;
using System.Threading.Tasks;

namespace Services
{
    class BaseService<Request, Response> : IServiceHandler<Request, Response>
        where Request : class
        where Response : class
    {
        protected Func<Request, Response> RequestFunc { get; }
        protected IServiceProvider Provider { get; }

        public BaseService(IServiceProvider provider)
        {
            Provider = provider;
        }

        public BaseService(Func<Request, Response> requestFunc)
        {
            RequestFunc = requestFunc;
        }

        public virtual Response Invoke(Request request)
        {
            return DoRequest(request);
        }

        public virtual async Task<Response> InvokeAsync(Request request)
        {
            return await Task.Run<Response>(() => { return DoRequest(request); }); ;
        }

        public virtual void Invoke(Request request, IServiceResponse<Response> service)
        {
            DoRequest(request, service);
        }

        public virtual async Task InvokeAsync(Request request, IServiceResponse<Response> service)
        {
            await Task.Run(() => { DoRequest(request, service); });
        }                

        protected virtual void DoRequest(Request request, IServiceResponse<Response> service)
        {
            service.ServiceResponse(DoRequest(request));
        }

        protected virtual Response DoRequest(Request request)
        {
            return RequestFunc?.Invoke(request);
        }
    }
}
