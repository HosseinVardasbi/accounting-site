using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLayer02.Domain;

namespace HamedProject02.Data
{
    public class HamedProject02Context : DbContext
    {
        public HamedProject02Context (DbContextOptions<HamedProject02Context> options)
            : base(options)
        {
        }

        public DbSet<DataLayer02.Domain.Customer> Customer { get; set; } = default!;

        public DbSet<DataLayer02.Domain.Factor>? Factor { get; set; }

        public DbSet<DataLayer02.Domain.Payment>? Payment { get; set; }

        public DbSet<DataLayer02.Domain.User>? User { get; set; }

        public DbSet<DataLayer02.Domain.Debt>? Debt { get; set; }
    }
}
