using PaymentGateway.API.Entities;
using System.Threading.Tasks;

namespace PaymentGateway.API.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest paymentRequest);

        Task<Payment> GetPaymentAsync(string processId);
    }
}