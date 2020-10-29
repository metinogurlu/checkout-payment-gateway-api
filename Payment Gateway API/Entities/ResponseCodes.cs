namespace PaymentGatewayAPI.Entities
{
    public static class ResponseCodes
    {
        public static ResponseCode Approved => new ResponseCode
        {
            Code = 10000,
            Message = "Approved"
        };

        public static class SoftDecline
        {
            public static ResponseCode InvalidCardNumber => new ResponseCode
            {
                Code = 20014,
                Message = "Invalid Card Number"
            };
        }

        public static class HardDecline
        {
        }

        public static class RiskResponses
        {
            public static ResponseCode CvvMissingOrIncorrect => new ResponseCode
            {
                Code = 40104,
                Message = "CVV is missing or incorrect"
            };
        }
    }
}