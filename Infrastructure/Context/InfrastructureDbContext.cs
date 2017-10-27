using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Context
{
    public class InfrastructureDbContext : IdentityDbContext<IdentityUser>, IInfrastructureDbContext
    {
        public InfrastructureDbContext() : base()
        { }

        public InfrastructureDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
