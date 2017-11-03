using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IInfrastructureDbContext : IEfIdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>        
    {
        DbSet<MessageModel> Messages { get; set; }
    }
}
