﻿using System;
using System.Text.Json.Serialization;

namespace PaymentGatewayAPI.Entities
{
    public class ProcessPaymentResponse
    {
        public Guid Id { get; private set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        public bool Approved { get; private set; }

        public string Status { get; private set; }

        [JsonPropertyName("response_code")]
        public int Code { get; set; }

        [JsonPropertyName("response_summary")]
        public string Summary { get; set; }

        public ProcessPaymentResponse(Guid id, decimal amount, string currency, ResponseCode gatewayValidationResponseCode)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
            Code = gatewayValidationResponseCode.Code;
            Summary = gatewayValidationResponseCode.Message;
            Approved = gatewayValidationResponseCode == ResponseCodes.Approved;
            Status = Approved ? "Successful" : "Unsuccessful";
        }
    }
}