using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayAPI.Entities;
using PaymentGatewayAPI.Services;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Makes a payment with given information
        /// </summary>
        /// <param name="paymentsRequest"></param>
        /// <returns>Payment</returns>
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest paymentRequest)
        {
            Payment payment = await _paymentService.ProcessPaymentAsync(paymentRequest);

            return new JsonResult(payment);
        }

        /// <summary>
        /// Gets the specific payment with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Payment</returns>
        [HttpGet]
        public async Task<IActionResult> GetPayment(string id)
        {
            Payment payment = await _paymentService.GetPaymentAsync(id);

            return new JsonResult(payment);
        }
    }
}