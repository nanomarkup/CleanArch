
namespace Core.Entities
{   
    public interface IMessageEntity<IMessageEntityAttrs> : IBaseEntity<IMessageEntityAttrs>
        where IMessageEntityAttrs : IPoco
    {
        
    }
}
