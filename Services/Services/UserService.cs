using AutoMapper;
using Core.Interactors;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Services
{
    public class UserService
    {
        IServiceProvider Provider { get; }

        public UserService(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IServiceHandler<DtoServiceUserCreate, DtoServiceUserId> Create
        {
            get
            {
                return new BaseService<DtoServiceUserCreate, DtoServiceUserId>(x => 
                    Mapper.Map<DtoServiceUserId>(Provider.GetService<IUserInteractor>().Create(Mapper.Map<DtoUserInteractorCreate>(x))));
            }
        }
        
        public IServiceHandler<DtoServiceUserId, DtoServiceUserInfo> Retrieve
        {
            get
            {
                return new BaseService<DtoServiceUserId, DtoServiceUserInfo>(x => 
                    Mapper.Map<DtoServiceUserInfo>(Provider.GetService<IUserInteractor>().Retrieve(Mapper.Map<DtoUserInteractorId>(x))));
            }
        }
        
        public IServiceHandler<DtoServiceUserModify, DtoServiceUserId> Modify
        {
            get
            {
                return new BaseService<DtoServiceUserModify, DtoServiceUserId>(x => 
                    Mapper.Map<DtoServiceUserId>(Provider.GetService<IUserInteractor>().Modify(Mapper.Map<DtoUserInteractorModify>(x))));
            }
        }

        public IServiceHandler<DtoServiceUserId, DtoServiceUserId> Delete
        {
            get
            {
                return new BaseService<DtoServiceUserId, DtoServiceUserId>(x => 
                    Mapper.Map<DtoServiceUserId>(Provider.GetService<IUserInteractor>().Delete(Mapper.Map<DtoUserInteractorId>(x))));
            }
        }        
    }

    public class DtoServiceUserId
    {
        public Guid Id { get; set; }
    }

    public class DtoServiceUserInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoServiceUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoServiceUserModify
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
