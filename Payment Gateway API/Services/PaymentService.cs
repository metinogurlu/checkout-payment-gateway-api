using PaymentGatewayAPI.Entities;
using PaymentGatewayAPI.Services;
using PaymentGatewayAPI.Validators;

namespace PaymentGatewayAPI
{
    public class PaymentServie : IPaymentService
    {
        private readonly ICardValidator _cardValidator;
        private readonly IProcessPaymentRequestValidator _processPaymentRequestValidator;

        public PaymentServie(IProcessPaymentRequestValidator processPaymentRequestValidator, ICardValidator cardValidator)
        {
            _cardValidator = cardValidator;
            _processPaymentRequestValidator = processPaymentRequestValidator;
        }

        public ResponseCode ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            return ValidatePaymentRequest(processPaymentRequest);
        }

        public ResponseCode ValidatePaymentRequest(ProcessPaymentRequest processPaymentRequest)
        {
            if (_processPaymentRequestValidator.isValid(processPaymentRequest))
                return ResponseCodes.Approved;

            if (!_processPaymentRequestValidator.IsCardValid(processPaymentRequest.Card))
                return _processPaymentRequestValidator.ValidateCard(processPaymentRequest.Card);
            else if (!_processPaymentRequestValidator.isAmountValid(processPaymentRequest.Amount))
                return ResponseCodes.SoftDecline.InvalidAmount;
            else
                return ResponseCodes.SoftDecline.UnsupportedCurrency;
        }
    }
}