using BS.Transactions.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Repositories.DBContexts
{
    public class AccountsTransactionsDBContext : DbContext
    {
        public AccountsTransactionsDBContext(DbContextOptions<AccountsTransactionsDBContext> options)
            : base(options)
        { }

        public virtual DbSet<AccountTransaction> AccountsTransactions { get; set; }
        public virtual DbSet<AccountBalance> AccountsBalances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
