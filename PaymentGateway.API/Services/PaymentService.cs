using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Data;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Services;
using PaymentGateway.API.Validators;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.API
{
    public class PaymentServie : IPaymentService
    {
        private readonly IProcessPaymentRequestValidator _processPaymentRequestValidator;
        private readonly IAcquiringBankSimulator _acquiringBankService;
        private readonly PaymentContext _paymentContext;
        private readonly ILogger<PaymentServie> _logger;

        public PaymentServie(ILogger<PaymentServie> logger,
            IProcessPaymentRequestValidator processPaymentRequestValidator,
            IAcquiringBankSimulator acquiringBankService,
            PaymentContext paymentContext)
        {
            _acquiringBankService = acquiringBankService;
            _processPaymentRequestValidator = processPaymentRequestValidator;
            _paymentContext = paymentContext;
            _logger = logger;
        }

        /// <summary>
        /// Process payment request and send it to bank if it is valid request
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public async Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest paymentRequest)
        {
            ResponseCode gatewayResponseCode = ValidatePaymentRequest(paymentRequest);

            //if the request is not valid return back with response immediately
            if (gatewayResponseCode.Equals(ResponseCodes.Approved))
                return await ProcessValidPaymentRequest(paymentRequest);
            else
                return ProcessInvalidPaymentRequest(paymentRequest, gatewayResponseCode);
        }

        private Payment ProcessInvalidPaymentRequest(ProcessPaymentRequest paymentRequest, ResponseCode gatewayResponseCode)
        {
            var unsuccessfulPayment = new Payment
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                CardNumber = paymentRequest.Card.CardNumber,
                ResponseCode = gatewayResponseCode.Code,
                ResponseSummary = gatewayResponseCode.Message,
                Status = "Unsuccessful",
                ProcessedAt = DateTime.Now
            };

            _logger.LogWarning("Payment request was not valid", unsuccessfulPayment);

            return unsuccessfulPayment;
        }

        private async Task<Payment> ProcessValidPaymentRequest(ProcessPaymentRequest paymentRequest)
        {
            Payment processedPayment;
            try
            {
                processedPayment = _acquiringBankService.ProcessPayment(paymentRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment request could not been processed on the banking phase", paymentRequest);
                throw;
            }
            try
            {
                _paymentContext.Payments.Add(processedPayment);
                await _paymentContext.SaveChangesAsync();

                _logger.LogInformation("Payment processed successfully", processedPayment);

                return processedPayment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment has been made but payment could not write to the database", paymentRequest);
                throw;
            }
        }

        /// <summary>
        /// Gets the specific payment with given processId
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public Task<Payment> GetPaymentAsync(string processId)
        {
            if (!Guid.TryParse(processId, out Guid processGuid))
                throw new ArgumentException("Given processId is not valid for this GetPayment request", nameof(processId));

            return GetPaymentByValidIdAsync(processId);
        }

        private async Task<Payment> GetPaymentByValidIdAsync(string processId)
        {
            return await _paymentContext.Payments.
                FirstOrDefaultAsync(p => p.ProcessId == Guid.Parse(processId));
        }

        /// <summary>
        /// Validates process payment request by card, amount and currency
        /// </summary>
        /// <param name="processPaymentRequest"></param>
        /// <returns></returns>
        private ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest)
        {
            if (_processPaymentRequestValidator.isValid(processPaymentRequest))
                return ResponseCodes.Approved;
            else if (!_processPaymentRequestValidator.IsCardValid(processPaymentRequest.Card))
                return _processPaymentRequestValidator.ValidateCard(processPaymentRequest.Card);
            else if (!_processPaymentRequestValidator.isAmountValid(processPaymentRequest.Amount))
                return ResponseCodes.SoftDecline.InvalidAmount;
            else
                return ResponseCodes.SoftDecline.UnsupportedCurrency;
        }
    }
}