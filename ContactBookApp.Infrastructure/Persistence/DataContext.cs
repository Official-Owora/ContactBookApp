using ContactBookApp.Domain.Entities;
using ContactBookApp.Domain.Shared;
using ContactBookApp.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        DbSet<Contact> Contacts { get; set; }
        DbSet<User> Users { get; set; }
       /* DbSet<CloudinarySettings> CloudinarySettingss { get; set; }*/
    }
}
