using Newtonsoft.Json;

namespace PaymentGatewayAPI.Entities
{
    public class ProcessPaymentRequest
    {
        public Card Card { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}