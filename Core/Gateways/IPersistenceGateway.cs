using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateways
{
    public interface IPersistenceGateway<Dto> where Dto : class
    {
        void Create(Dto dto);
        void Retrieve(Dto dto);
        void Modify(Dto dto);
        void Delete(Dto dto);
    }
}
