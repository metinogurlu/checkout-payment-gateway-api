using PaymentGatewayAPI.Data;
using PaymentGatewayAPI.Entities;
using PaymentGatewayAPI.Services;
using PaymentGatewayAPI.Validators;
using System;

namespace PaymentGatewayAPI
{
    public class PaymentServie : IPaymentService
    {
        private readonly IProcessPaymentRequestValidator _processPaymentRequestValidator;
        private readonly IAcquiringBankSimulator _acquiringBankService;
        private readonly PaymentContext _paymentContext;

        public PaymentServie(IProcessPaymentRequestValidator processPaymentRequestValidator, IAcquiringBankSimulator acquiringBankService, PaymentContext paymentContext)
        {
            _acquiringBankService = acquiringBankService;
            _processPaymentRequestValidator = processPaymentRequestValidator;
            _paymentContext = paymentContext;
        }

        public Payment ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            ResponseCode gatewayResponseCode = ValidatePaymentRequest(paymentRequest);

            //if the request is not valid return back with response immediately
            if (!gatewayResponseCode.Equals(ResponseCodes.Approved))
                return new Payment
                {
                    Amount = paymentRequest.Amount,
                    Currency = paymentRequest.Currency,
                    CardNumber = paymentRequest.Card.CardNumber,
                    ResponseCode = gatewayResponseCode.Code,
                    ResponseSummary = gatewayResponseCode.Message,
                    Status = "Unsuccessful",
                    ProcessedAt = DateTime.Now
                };

            Payment processedPayment = _acquiringBankService.ProcessPayment(paymentRequest);

            _paymentContext.Payments.Add(processedPayment);
            _paymentContext.SaveChanges();

            return processedPayment;
        }

        public ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest)
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