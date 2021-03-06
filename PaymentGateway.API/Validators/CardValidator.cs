﻿using CreditCardValidator;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Entities;
using System;
using System.Text.RegularExpressions;

namespace PaymentGateway.API.Validators
{
    public class CardValidator : ICardValidator
    {
        private readonly ILogger<CardValidator> _logger;

        public bool IsValid(Card card)
            => IsCardNumberValid(card.CardNumber) && IsExpiryDateValid(card.ExpirationMonth, card.ExpirationYear) && IsCvvValid(card.Cvv);

        public bool IsCardNumberValid(string cardNumber)
        {
            try
            {
                return new CreditCardDetector(cardNumber).IsValid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CardNumberValidationFail", cardNumber);
                return false;
            }
        }

        public bool IsCvvValid(string cvv)
            => new Regex(@"^\d{3}$").IsMatch(cvv);

        public CardValidator(ILogger<CardValidator> logger)
        {
            _logger = logger;
        }

        public bool IsExpiryDateValid(int month, int year)
        {
            try
            {
                var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month);
                var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

                //check expiry greater than today & goes 20 years into the future.
                return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(20));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExpiryDateValidationFail", month, year);
                return false;
            }
        }
    }
}