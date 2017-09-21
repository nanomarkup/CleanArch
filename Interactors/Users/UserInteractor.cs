using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;
using Core.Interactors;
using Core.Gateways;

namespace Interactors
{
    public class UserInteractor : BaseInteractor, IUserInteractor
    {
        public UserInteractor(IServiceProvider provider) : base(provider) { }

        public Guid Create(string firstName, string lastName, string email)
        {
            var user = GetService<IUserEntity>();            
            user.Changed += (sender, args) =>
            {
                GetService<IPersistenceGateway<DtoUserEntity>>().Create(new DtoUserEntity());
            };

            return user.Create(new DtoUserCreate()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });
        }

        public void Update(Guid id, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
