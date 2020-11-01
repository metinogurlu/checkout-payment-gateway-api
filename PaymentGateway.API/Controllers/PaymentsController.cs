using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Services;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.API.Controllers
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
            try
            {
                Payment payment = await _paymentService.ProcessPaymentAsync(paymentRequest);

                return new JsonResult(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment could not processed!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the specific payment with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Payment</returns>
        [HttpGet]
        public async Task<IActionResult> GetPayment(string id)
        {
            try
            {
                Payment payment = await _paymentService.GetPaymentAsync(id);

                if (payment == default)
                    return NotFound();

                return new JsonResult(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment could not retrieved!", id);
                throw;
            }
        }
    }
}