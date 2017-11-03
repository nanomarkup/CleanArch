using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Context
{
    public interface IEfIdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken> : IEfDbContext
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        DbSet<TUser> Users { get; set; }
        DbSet<TUserClaim> UserClaims { get; set; }
        DbSet<TUserLogin> UserLogins { get; set; }
        DbSet<TUserToken> UserTokens { get; set; }
    }
}
