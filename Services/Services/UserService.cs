using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Interactors;

namespace Services
{
    public class UserService
    {
        IServiceProvider Provider { get; }

        public UserService(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IServiceHandler<DtoUserCreateInteractor, DtoUserIdInteractor> Create
        {
            get
            {
                return new BaseService<DtoUserCreateInteractor, DtoUserIdInteractor>(x => Provider.GetService<IUserInteractor>().Create(x));
            }
        }
        
        public IServiceHandler<DtoUserIdInteractor, DtoUserInfoInteractor> Retrieve
        {
            get
            {
                return new BaseService<DtoUserIdInteractor, DtoUserInfoInteractor>(x => Provider.GetService<IUserInteractor>().Retrieve(x));
            }
        }
        
        public IServiceHandler<DtoUserModifyInteractor, DtoUserIdInteractor> Modify
        {
            get
            {
                return new BaseService<DtoUserModifyInteractor, DtoUserIdInteractor>(x => Provider.GetService<IUserInteractor>().Modify(x));
            }
        }

        public IServiceHandler<DtoUserIdInteractor, DtoUserIdInteractor> Delete
        {
            get
            {
                return new BaseService<DtoUserIdInteractor, DtoUserIdInteractor>(x => Provider.GetService<IUserInteractor>().Delete(x));
            }
        }        
    }
}
