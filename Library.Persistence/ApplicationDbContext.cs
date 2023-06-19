using Library.Domain.Entities;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>,
        IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(_connectionString, serverOptions => serverOptions.MigrationsAssembly(_migrationAssembly));
            }
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Book> Books { get; set; }
    }
}
