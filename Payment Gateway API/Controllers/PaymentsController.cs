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
            return new JsonResult(_paymentService.ProcessPayment(paymentRequest));
        }
    }
}