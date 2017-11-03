using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
