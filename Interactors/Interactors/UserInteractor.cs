using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;
using Core.Models;
using System;

namespace Interactors
{
    public class UserInteractor : BaseInteractor, IUserInteractor
    {
        public UserInteractor(IServiceProvider provider) : base(provider) { }

        public DtoUserIdInteractor Create(DtoUserCreateInteractor dto)
        {
            var model = Mapper.Map<UserModel>(dto);
            model.Id = Guid.NewGuid();
            model.Created = DateTime.Now;
            model.Modified = model.Created;

            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) => 
            {
                // Add a new user to the infrastructure
                GetService<IUserGateway>().Add(user.Attrs as UserModel);
            };
            user.Create(model);
            return new DtoUserIdInteractor() { Id = user.Attrs.Id };
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
            user.Initialize(Mapper.Map<UserModel>(userInfo));
            user.Changed += (sender, args) =>
            {
                // Modify user in the infrastructure
                GetService<IUserGateway>().Modify(Mapper.Map<DtoUserModifiedGateway>(user));
            };

            // Update all fields
            user.BeginUpdate();
            try
            {
                user.Attrs.FirstName = dto.FirstName;
                user.Attrs.LastName = dto.LastName;
                user.Attrs.Email = dto.Email;
            }
            finally
            {
                user.EndUpdate();
            }
            return new DtoUserIdInteractor() { Id = user.Attrs.Id };
        }

        public DtoUserIdInteractor Delete(DtoUserIdInteractor id)
        {
            return new DtoUserIdInteractor() { Id = GetService<IUserGateway>().Delete(id.Id) };
        }
    }
}
