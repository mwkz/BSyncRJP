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
#pragma warning restore 612, 618
        }
    }
}
