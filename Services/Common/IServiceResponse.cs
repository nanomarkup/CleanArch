
namespace Services
{
    public interface IServiceResponse<Response>
    {
        void ServiceResponse(Response dto);
    }
}
