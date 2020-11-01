using PaymentGatewayAPI.Entities;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest paymentRequest);

        Task<Payment> GetPaymentAsync(string processId);
    }
}