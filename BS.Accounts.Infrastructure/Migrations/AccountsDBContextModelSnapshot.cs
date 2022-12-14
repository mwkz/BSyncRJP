// <auto-generated />
using System;
using BS.Accounts.Infrastructure.Repositories.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BS.Accounts.Infrastructure.Migrations
{
    [DbContext(typeof(AccountsDBContext))]
    partial class AccountsDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("BS.Accounts.Core.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountNo")
                        .IsUnique();

                    b.ToTable("Account", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
