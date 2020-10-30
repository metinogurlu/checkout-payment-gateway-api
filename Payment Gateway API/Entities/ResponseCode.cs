namespace PaymentGatewayAPI.Entities
{
    public class ResponseCode
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ResponseCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}