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

        public IServiceHandler<DtoIUserCreate, DtoIUserId> Create
        {
            get
            {
                return new BaseService<DtoIUserCreate, DtoIUserId>(x => Provider.GetService<IUserInteractor>().Create(x));
            }
        }
        
        public IServiceHandler<DtoIUserId, DtoIUserInfo> Retrieve
        {
            get
            {
                return new BaseService<DtoIUserId, DtoIUserInfo>(x => Provider.GetService<IUserInteractor>().Retrieve(x));
            }
        }
        
        public IServiceHandler<DtoIUserModify, DtoIUserId> Modify
        {
            get
            {
                return new BaseService<DtoIUserModify, DtoIUserId>(x => Provider.GetService<IUserInteractor>().Modify(x));
            }
        }

        public IServiceHandler<DtoIUserId, DtoIUserId> Delete
        {
            get
            {
                return new BaseService<DtoIUserId, DtoIUserId>(x => Provider.GetService<IUserInteractor>().Delete(x));
            }
        }        
    }
}
