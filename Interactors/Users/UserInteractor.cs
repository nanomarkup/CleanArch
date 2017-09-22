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

        public Guid Create(DtoIUserCreate dto)
        {
            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) => 
            {
                // Add a new user to the infrastructure
                GetService<IUserGateway>().Add(Mapper.Map<DtoGUserInfo>(user));
            };
            return user.Create(Mapper.Map<DtoEUser>(dto));
        }

        public DtoIUserInfo Retrieve(Guid id)
        {
            return Mapper.Map<DtoIUserInfo>(GetService<IUserGateway>().Retrieve(id));
        }

        public Guid Modify(DtoIUserModify dto)
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
            return user.Identity.Id;
        }

        public Guid Delete(Guid id)
        {
            return GetService<IUserGateway>().Delete(id);
        }
    }
}
