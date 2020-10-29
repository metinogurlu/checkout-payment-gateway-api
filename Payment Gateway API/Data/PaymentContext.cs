using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Data
{
    public class PaymentContext : DbContext
    {
        public DbSet<PaymentResponse> PaymentTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}