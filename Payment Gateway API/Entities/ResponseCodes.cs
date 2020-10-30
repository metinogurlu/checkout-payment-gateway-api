namespace PaymentGatewayAPI.Entities
{
    public static class ResponseCodes
    {
        public static ResponseCode Approved => new ResponseCode(10000, "Approved");

        public static class SoftDecline
        {
            public static ResponseCode InvalidCardNumber => new ResponseCode(20014, "Invalid Card Number");

            public static ResponseCode InvalidAmount => new ResponseCode(20013, "Invalid Value/Amount");

            public static ResponseCode UnsupportedCurrency => new ResponseCode(20106, "Unsupported currency");
        }

        public static class HardDecline
        {
        }

        public static class RiskResponses
        {
            public static ResponseCode CvvMissingOrIncorrect => new ResponseCode(40104, "CVV is missing or incorrect");
        }
    }
}