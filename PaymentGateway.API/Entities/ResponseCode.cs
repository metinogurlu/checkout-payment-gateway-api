using System;

namespace PaymentGateway.API.Entities
{
    public sealed class ResponseCode : IEquatable<ResponseCode>
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ResponseCode(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public bool Equals(ResponseCode other)
        {
            return !(other is null) &&
                   Code == other.Code &&
                   Message == other.Message;
        }
    }
}