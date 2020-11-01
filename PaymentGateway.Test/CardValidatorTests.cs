using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Validators;
using System;
using Xunit;

namespace PaymentGateway.Test
{
    public class CardValidatorTests
    {
        [Theory, AutoMoqData]
        public void Card_Should_Valid_With_Valid_Information(Mock<ILogger<CardValidator>> loggerMock)
        {
            ICardValidator cardValidator = new CardValidator(loggerMock.Object);

            var card = new Card
            {
                CardNumber = "4242424242424242",
                Cvv = "100",
                ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                ExpirationYear = DateTime.Now.AddMonths(1).Year
            };

            Assert.True(cardValidator.IsValid(card));
        }

        [Theory, AutoMoqData]
        public void Card_Should_Not_Valid_With_InValid_Information(Mock<ILogger<CardValidator>> loggerMock)
        {
            ICardValidator cardValidator = new CardValidator(loggerMock.Object);

            var card = new Card
            {
                CardNumber = "4442424242424242",
                Cvv = "201",
                ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                ExpirationYear = DateTime.Now.AddMonths(1).Year
            };

            Assert.False(cardValidator.IsValid(card));
        }
    }
}