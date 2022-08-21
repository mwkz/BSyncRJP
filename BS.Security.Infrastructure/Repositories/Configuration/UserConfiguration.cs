using BS.Security.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Infrastructure.Repositories.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasData(
                new User { 
                    Id = 1,
                    Username = "admin", 
                    Password = "admin", 
                    CreatedDate = DateTime.UtcNow, 
                    UpdatedDate = DateTime.UtcNow, 
                    Enabled = true });


            builder.HasData(
                new User
                {
                    Id = 2,
                    Username = "user",
                    Password = "user",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Enabled = true
                });



        }
    }
}
