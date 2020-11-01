using Moq;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Validators;
using System;
using Xunit;

namespace PaymentGateway.Test
{
    public class ProcessPaymentRequestValidatorTests
    {
        [Theory, AutoMoqData]
        public void Process_Payment_Request_Should_Valid_With_Valid_Information(Mock<ICardValidator> cardValidatorMock)
        {
            var sut = new ProcessPaymentRequestValidator(cardValidatorMock.Object);

            var processPaymentRequest = new ProcessPaymentRequest
            {
                Card = new Card
                {
                    CardNumber = "4242424242424242",
                    Cvv = "100",
                    ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                    ExpirationYear = DateTime.Now.AddMonths(1).Year
                },
                Amount = 100,
                Currency = "USD"
            };

            cardValidatorMock.Setup(c => c.IsValid(processPaymentRequest.Card)).Returns(true);

            Assert.True(sut.IsValid(processPaymentRequest));
        }

        [Theory, AutoMoqData]
        public void Process_Payment_Request_Should_Not_Valid_With_InValid_Information(Mock<ICardValidator> cardValidatorMock)
        {
            var sut = new ProcessPaymentRequestValidator(cardValidatorMock.Object);

            var card = new Card
            {
                CardNumber = "4242424242424242",
                Cvv = "100",
                ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                ExpirationYear = DateTime.Now.AddMonths(1).Year
            };

            var processPaymentRequest = new ProcessPaymentRequest
            {
                Card = card,
                Amount = 100,
                Currency = ""
            };

            Assert.False(sut.IsValid(processPaymentRequest));
        }
    }
}