using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayAPI.Entities;

namespace PaymentGatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ILogger<PaymentsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Makes a payment with given information
        /// </summary>
        /// <param name="paymentsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessPayment([FromBody] ProcessPaymentRequest paymentRequest)
        {
            return Ok(paymentRequest);
        }
    }
}