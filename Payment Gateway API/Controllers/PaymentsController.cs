using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayAPI.Entities;
using PaymentGatewayAPI.Services;

namespace PaymentGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IAcquiringBankSimulator _acquiringBankService;

        public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Makes a payment with given information
        /// </summary>
        /// <param name="paymentsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessPayment([FromBody] ProcessPaymentRequest paymentRequest)
        {
            ProcessPaymentResponse processPaymentResponseMessage = _paymentService.ProcessPayment(paymentRequest);

            return new JsonResult(processPaymentResponseMessage);
        }
    }
}