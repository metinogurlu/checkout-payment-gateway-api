using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Data
{
    public class PaymentContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentContext(DbContextOptions options) : base(options)
        {
        }
    }
}