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

        public DtoIUserId Create(DtoIUserCreate dto)
        {
            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) => 
            {
                // Add a new user to the infrastructure
                GetService<IUserGateway>().Add(Mapper.Map<DtoGUserInfo>(user));
            };
            return new DtoIUserId() { Id = user.Create(Mapper.Map<DtoEUser>(dto)) };
        }

        public DtoIUserInfo Retrieve(DtoIUserId id)
        {
            return Mapper.Map<DtoIUserInfo>(GetService<IUserGateway>().Retrieve(id.Id));
        }

        public DtoIUserId Modify(DtoIUserModify dto)
        {            
            // Read user from the infrastructure
            var user = GetService<IUserEntity>();
            var userInfo = GetService<IUserGateway>().Retrieve(dto.Id);
            user.Initialize(Mapper.Map<DtoEUserIdentity>(userInfo));
            user.Changed += (sender, args) =>
            {
                // Modify user in the infrastructure
                GetService<IUserGateway>().Modify(Mapper.Map<DtoGUserModified>(user));
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
            return new DtoIUserId() { Id = user.Identity.Id };
        }

        public DtoIUserId Delete(DtoIUserId id)
        {
            return new DtoIUserId() { Id = GetService<IUserGateway>().Delete(id.Id) };
        }
    }
}
