using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class Core2DbContext : DbContext
    {
        public Core2DbContext(DbContextOptions<Core2DbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<DataMaintainer> DataMaintainers { get; set; }
        public DbSet<DataConsultant> DataConsultants { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanPayment> LoanPayments { get; set; }

    }
}