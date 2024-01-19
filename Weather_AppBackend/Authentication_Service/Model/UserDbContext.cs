using Microsoft.EntityFrameworkCore;

namespace Authentication_Service.Model
{
    public class UserDbContext :DbContext
    {
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
    }
}
