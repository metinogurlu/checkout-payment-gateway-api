using System;
using System.Text.Json.Serialization;

namespace PaymentGatewayAPI.Entities
{
    public class ProcessPaymentRequest
    {
        public Guid Id { get; private set; }

        public Card Card { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        public ProcessPaymentRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}