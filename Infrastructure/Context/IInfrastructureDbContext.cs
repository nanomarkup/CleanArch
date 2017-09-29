using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Context
{
    public interface IInfrastructureDbContext : IEfIdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>        
    {
        DbSet<MessageEntity> Messages { get; set; }
    }
}
