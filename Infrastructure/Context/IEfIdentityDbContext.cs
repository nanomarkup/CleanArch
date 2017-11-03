using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Context
{
    public interface IEfIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> :
        IEfIdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        DbSet<TUserRole> UserRoles { get; set; }
        DbSet<TRole> Roles { get; set; }
        DbSet<TRoleClaim> RoleClaims { get; set; }
    }
}
