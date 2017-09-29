using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Context
{
    public class InfrastructureDbContext : IdentityDbContext<IdentityUser>, IInfrastructureDbContext
    {
        public InfrastructureDbContext() : base()
        { }

        public InfrastructureDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<MessageEntity> Messages { get; set; }
    }
}
