using PaymentGatewayAPI.Validators;
using System.Text.Json.Serialization;

namespace PaymentGatewayAPI.Entities
{
    public class Card
    {
        [JsonPropertyName("card_number")]
        public string CardNumber { get; set; }

        [JsonPropertyName("expiry_month")]
        public int ExpirationMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public int ExpirationYear { get; set; }

        [JsonPropertyName("cvv")]
        public string Cvv { get; set; }

        private readonly ICreditCardValidator _validator;

        public Card(ICreditCardValidator validator)
        {
            _validator = validator;
        }

        public ResponseCode Validate()
        {
            if (_validator.isValid(this))
                return ResponseCodes.Approved;

            if (!_validator.isCardNumberValid(CardNumber))
                return ResponseCodes.SoftDecline.InvalidCardNumber;
            else if (!_validator.isExpiryDateValid(ExpirationMonth, ExpirationYear))
                return ResponseCodes.SoftDecline.InvalidCardNumber;
            else
                return ResponseCodes.RiskResponses.CvvMissingOrIncorrect;
        }
    }
}