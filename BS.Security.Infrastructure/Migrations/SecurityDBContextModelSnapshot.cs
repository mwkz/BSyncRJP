// <auto-generated />
using System;
using BS.Security.Infrastructure.Repositories.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BS.Security.Infrastructure.Migrations
{
    [DbContext(typeof(SecurityDBContext))]
    partial class SecurityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("BS.Security.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9687),
                            Enabled = true,
                            Password = "admin",
                            UpdatedDate = new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9688),
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9703),
                            Enabled = true,
                            Password = "user",
                            UpdatedDate = new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9703),
                            Username = "user"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
