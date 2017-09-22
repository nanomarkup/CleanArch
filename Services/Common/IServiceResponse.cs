
namespace Services
{
    public interface IServiceResponse<DtoResponse>
    {
        void ServiceResponse(DtoResponse dto);
    }
}
