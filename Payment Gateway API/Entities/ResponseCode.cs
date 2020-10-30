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

        public override bool Equals(object obj)
        {
            return (obj is ResponseCode) && (obj as ResponseCode).Code.Equals(Code);
        }

        public static bool operator ==(ResponseCode v1, ResponseCode v2)
        {
            return v1.Code.Equals(v2.Code);
        }

        public static bool operator !=(ResponseCode v1, ResponseCode v2)
        {
            return !v1.Code.Equals(v2.Code);
        }
    }
}