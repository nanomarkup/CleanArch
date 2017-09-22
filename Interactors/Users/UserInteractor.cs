using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;

namespace Interactors
{
    public class UserInteractor : BaseInteractor, IUserInteractor
    {
        public UserInteractor(IServiceProvider provider) : base(provider) { }

        public Guid Create(DtoUserIntCreate dto)
        {
            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) =>
            {
                GetService<IUserGateway>().Add(new DtoUserGwInfo()
                {
                    Id = user.Identity.Id,
                    Created = user.Identity.Created,
                    Modified = user.Identity.Modified,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            };

            return user.Create(new DtoUserEntity()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            });
        }

        public void Modify(DtoUserIntModify dto)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DtoUserIntDelete dto)
        {
            throw new NotImplementedException();
        }
    }
}
