namespace PaymentGatewayAPI.Entities
{
    public class ProcessPaymentResponse
    {
        public ProcessPaymentRequest PaymentRequest { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseSummary { get; set; }
    }
}