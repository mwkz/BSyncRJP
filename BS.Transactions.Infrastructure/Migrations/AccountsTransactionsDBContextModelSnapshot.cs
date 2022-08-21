﻿// <auto-generated />
using System;
using BS.Transactions.Infrastructure.Repositories.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BS.Transactions.Infrastructure.Migrations
{
    [DbContext(typeof(AccountsTransactionsDBContext))]
    partial class AccountsTransactionsDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("BS.Transactions.Core.Models.AccountBalance", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("AccountBalance", (string)null);
                });

            modelBuilder.Entity("BS.Transactions.Core.Models.AccountTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Credit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Debit")
                        .HasColumnType("TEXT");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountTransaction", (string)null);
                });

            modelBuilder.Entity("BS.Transactions.Core.Models.AccountTransaction", b =>
                {
                    b.HasOne("BS.Transactions.Core.Models.AccountBalance", "AccountBalance")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountBalance");
                });

            modelBuilder.Entity("BS.Transactions.Core.Models.AccountBalance", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
