using Microsoft.EntityFrameworkCore;
using PaymentGateway.API.Entities;

namespace PaymentGateway.API.Data
{
    public class PaymentContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentContext(DbContextOptions options) : base(options)
        {
        }
    }
}