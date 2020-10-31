using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGatewayAPI.Entities
{
    [Table("payments")]
    public class Payment
    {
        [JsonIgnore]
        [Column("id")]
        public long Id { get; set; }

        [Column("identifier")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Identifier { get; set; }

        [Column("card_number")]
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("response_code")]
        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        [Column("response_summary")]
        [JsonProperty("response_summary")]
        public string ResponseSummary { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("processed_at")]
        [JsonProperty("processed_at")]
        public DateTime ProcessedAt { get; set; }
    }
}