using BS.Transactions.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Repositories.Configuration
{
    public class AccountBalanceConfiguration : IEntityTypeConfiguration<AccountBalance>
    {
        public void Configure(EntityTypeBuilder<AccountBalance> builder)
        {
            builder.ToTable(nameof(AccountBalance));

            builder.HasKey(b => b.AccountId);

            builder.HasMany(b => b.Transactions)
                   .WithOne(t => t.AccountBalance)
                   .HasForeignKey(t => t.AccountId);
        }
    }
}
