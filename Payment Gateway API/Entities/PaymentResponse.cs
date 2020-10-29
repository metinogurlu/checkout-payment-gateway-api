using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Entities
{
    public class PaymentResponse
    {
        public PaymentRequest PaymentRequest { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseSummary { get; set; }
    }
}