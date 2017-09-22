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
                GetService<IUserGateway>().Add(Mapper.Map<DtoGUserInfo>(user));
            };
            return user.Create(Mapper.Map<DtoEUser>(dto));
        }

        public void Modify(DtoIUserModify dto)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DtoIUserDelete dto)
        {
            throw new NotImplementedException();
        }
    }
}
