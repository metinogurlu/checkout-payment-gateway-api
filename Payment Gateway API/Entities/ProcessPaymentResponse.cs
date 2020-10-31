using Newtonsoft.Json;

namespace PaymentGatewayAPI.Entities
{
    public class ProcessPaymentResponse
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        public bool Approved { get; private set; }

        public string Status { get; private set; }

        [JsonProperty("response_code")]
        public int Code { get; set; }

        [JsonProperty("response_summary")]
        public string Summary { get; set; }

        public ProcessPaymentResponse(decimal amount, string currency, ResponseCode gatewayValidationResponseCode)
        {
            Amount = amount;
            Currency = currency;
            Code = gatewayValidationResponseCode.Code;
            Summary = gatewayValidationResponseCode.Message;
            Approved = gatewayValidationResponseCode.Equals(ResponseCodes.Approved);
            Status = Approved ? "Successful" : "Unsuccessful";
        }
    }
}