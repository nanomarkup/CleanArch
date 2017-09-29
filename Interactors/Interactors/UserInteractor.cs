using System;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;
using AutoMapper;

namespace Interactors
{
    public class UserInteractor : BaseInteractor, IUserInteractor
    {
        public UserInteractor(IServiceProvider provider) : base(provider) { }

        public DtoUserIdInteractor Create(DtoUserCreateInteractor dto)
        {
            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) => 
            {
                // Add a new user to the infrastructure
                GetService<IUserGateway>().Add(Mapper.Map<DtoUserInfoGateway>(user));
            };
            return new DtoUserIdInteractor() { Id = user.Create(Mapper.Map<DtoUserEntity>(dto)) };
        }

        public DtoUserInfoInteractor Retrieve(DtoUserIdInteractor id)
        {
            return Mapper.Map<DtoUserInfoInteractor>(GetService<IUserGateway>().Retrieve(id.Id));
        }

        public DtoUserIdInteractor Modify(DtoUserModifyInteractor dto)
        {            
            // Read user from the infrastructure
            var user = GetService<IUserEntity>();
            var userInfo = GetService<IUserGateway>().Retrieve(dto.Id);
            user.Initialize(Mapper.Map<DtoUserIdentityEntity>(userInfo));
            user.Changed += (sender, args) =>
            {
                // Modify user in the infrastructure
                GetService<IUserGateway>().Modify(Mapper.Map<DtoUserModifiedGateway>(user));
            };

            // Update all fields
            user.BeginUpdate();
            try
            {
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
            }
            finally
            {
                user.EndUpdate();
            }
            return new DtoUserIdInteractor() { Id = user.Identity.Id };
        }

        public DtoUserIdInteractor Delete(DtoUserIdInteractor id)
        {
            return new DtoUserIdInteractor() { Id = GetService<IUserGateway>().Delete(id.Id) };
        }
    }
}
