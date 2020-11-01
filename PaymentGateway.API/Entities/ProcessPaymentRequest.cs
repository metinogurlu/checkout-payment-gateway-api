using Newtonsoft.Json;

namespace PaymentGateway.API.Entities
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