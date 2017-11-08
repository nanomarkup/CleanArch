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

        public DtoUserInteractorId Create(DtoUserInteractorCreate dto)
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
            return new DtoUserInteractorId() { Id = user.Attrs.Id };
        }

        public DtoUserInteractorInfo Retrieve(DtoUserInteractorId id)
        {
            return Mapper.Map<DtoUserInteractorInfo>(GetService<IUserGateway>().Retrieve(id.Id));
        }

        public DtoUserInteractorId Modify(DtoUserInteractorModify dto)
        {            
            // Read user from the infrastructure
            var user = GetService<IUserEntity>();
            var userInfo = GetService<IUserGateway>().Retrieve(dto.Id);
            user.Initialize(Mapper.Map<UserModel>(userInfo));
            user.Changed += (sender, args) =>
            {
                // Modify user in the infrastructure
                GetService<IUserGateway>().Modify(Mapper.Map<DtoUserGatewayModify>(user));
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
            return new DtoUserInteractorId() { Id = user.Attrs.Id };
        }

        public DtoUserInteractorId Delete(DtoUserInteractorId id)
        {
            return new DtoUserInteractorId() { Id = GetService<IUserGateway>().Delete(id.Id) };
        }
    }
}
