using Microsoft.EntityFrameworkCore;
using PaymentGateway.API.Data;

namespace PaymentGateway.Test
{
    public static class PaymentContextMocker
    {
        public static PaymentContext GetPaymentContext()
        {
            var options = new DbContextOptionsBuilder<PaymentContext>()
                .UseInMemoryDatabase("payments")
                .Options;

            var dbContext = new PaymentContext(options);

            dbContext.Seed();

            return dbContext;
        }
    }
}