using DataLayer02.Config;
using DataLayer02.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer02.Context
{
    public class DB_context : DbContext
    {
        //public DB_context()
        //{
        //}

        public DB_context(DbContextOptions<DB_context> options) : base(options)
        {

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Factor> factors { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Debt> debts { get; set; }
        public DbSet<DebtPayment> debtPayments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CustomerConfig());
            //modelBuilder.ApplyConfiguration(new FactorConfig());
            //modelBuilder.ApplyConfiguration(new PaymentConfig());
            //modelBuilder.Entity<Payment>().HasOne<Factor>().WithMany().HasForeignKey(p => p.FactorNo);
            //modelBuilder.Entity<Factor>().HasOne<Customer>().WithMany().HasForeignKey(p => p.customerId);

        }

    }
}
