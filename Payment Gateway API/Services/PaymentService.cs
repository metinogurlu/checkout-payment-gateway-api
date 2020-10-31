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

            //if the request is not valid return back with response immediately
            if (gatewayResponseCode != ResponseCodes.Approved)
                return new ProcessPaymentResponse(paymentRequest.Amount, paymentRequest.Currency, gatewayResponseCode);

            AcquiringBankResponse acquiringBankResponse = _acquiringBankService.ProcessPayment(paymentRequest);

            return new ProcessPaymentResponse(paymentRequest.Amount, paymentRequest.Currency, acquiringBankResponse.ResponseCode);
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