using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Entities
{
    public class PaymentRequest
    {
        public Guid Id { get; private set; }

        public Card Card { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        public PaymentRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}