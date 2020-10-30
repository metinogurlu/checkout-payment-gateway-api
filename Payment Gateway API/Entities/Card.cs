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
    }
}