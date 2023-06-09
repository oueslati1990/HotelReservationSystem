using AccountApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Data
{
    public class AuthContext : IdentityDbContext<AppUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options)
             : base(options)
        {
        }
    }
}
