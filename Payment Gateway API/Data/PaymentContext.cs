using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Data
{
    public class PaymentContext : DbContext
    {
        public DbSet<ProcessPaymentResponse> PaymentTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}