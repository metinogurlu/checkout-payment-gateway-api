using Newtonsoft.Json;

namespace PaymentGatewayAPI.Entities
{
    public class Card
    {
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("expiry_month")]
        public int ExpirationMonth { get; set; }

        [JsonProperty("expiry_year")]
        public int ExpirationYear { get; set; }

        [JsonProperty("cvv")]
        public string Cvv { get; set; }
    }
}