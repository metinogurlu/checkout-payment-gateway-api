using PaymentGatewayAPI.Entities;
using PaymentGatewayAPI.Services;
using PaymentGatewayAPI.Validators;

namespace PaymentGatewayAPI
{
    public class PaymentServie : IPaymentService
    {
        private readonly ICardValidator _cardValidator;
        private readonly IProcessPaymentRequestValidator _processPaymentRequestValidator;

        private readonly IAcquiringBankSimulator _acquiringBankService;

        public PaymentServie(IProcessPaymentRequestValidator processPaymentRequestValidator, ICardValidator cardValidator, IAcquiringBankSimulator acquiringBankService)
        {
            _cardValidator = cardValidator;
            _acquiringBankService = acquiringBankService;
            _processPaymentRequestValidator = processPaymentRequestValidator;
        }

        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            ResponseCode gatewayResponseCode = ValidatePaymentRequest(paymentRequest);

            if (gatewayResponseCode == ResponseCodes.Approved)
                gatewayResponseCode = _acquiringBankService.ProcessPayment(paymentRequest);

            var processPaymentResponseMessage =
                new ProcessPaymentResponse(paymentRequest.Id, paymentRequest.Amount, paymentRequest.Currency, gatewayResponseCode);

            return processPaymentResponseMessage;
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