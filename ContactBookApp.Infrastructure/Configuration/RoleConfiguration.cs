using ContactBookApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactBookApp.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = RoleOfTheUser.Admin.ToString(),
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = RoleOfTheUser.RegularUser.ToString(),
                    NormalizedName = "REGULARUSER",
                }
                ); 
        }
    }
}
