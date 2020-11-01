using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.API.Services;
using PaymentGateway.API.Data;
using PaymentGateway.API.Validators;
using Xunit;
using System.Threading.Tasks;
using PaymentGateway.API.Entities;
using System;
using PaymentGateway.API.Helpers;

namespace PaymentGateway.Test
{
    public class PaymentServiceTests
    {
        [Theory, AutoMoqData]
        public async Task GetPaymentAsync_Should_Return_Correct_Record(
            Mock<ILogger<PaymentService>> loggerMock,
            Mock<IProcessPaymentRequestValidator> processPaymentRequestValidatorMock,
            Mock<IAcquiringBankSimulator> acquiringBankSimulatorMock)
        {
            var paymentContext = PaymentContextMocker.GetPaymentContext();

            var sut = new PaymentService(loggerMock.Object,
                processPaymentRequestValidatorMock.Object,
                acquiringBankSimulatorMock.Object,
                paymentContext);

            Payment payment = await sut.GetPaymentAsync("c1583bf7-ee44-4006-8bb6-52b7d5a07643");

            Assert.EndsWith("4242", payment.CardNumber);
        }

        [Theory, AutoMoqData]
        public async Task GetPaymentAsync_Should_Throw_ArgumentException(
            Mock<ILogger<PaymentService>> loggerMock,
            Mock<IProcessPaymentRequestValidator> processPaymentRequestValidatorMock,
            Mock<IAcquiringBankSimulator> acquiringBankSimulatorMock)
        {
            var paymentContext = PaymentContextMocker.GetPaymentContext();

            var sut = new PaymentService(loggerMock.Object,
                processPaymentRequestValidatorMock.Object,
                acquiringBankSimulatorMock.Object,
                paymentContext);

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetPaymentAsync("asd"));
        }

        [Theory, AutoMoqData]
        public async Task GetPaymentAsync_Should_Return_Default_Payment(
            Mock<ILogger<PaymentService>> loggerMock,
            Mock<IProcessPaymentRequestValidator> processPaymentRequestValidatorMock,
            Mock<IAcquiringBankSimulator> acquiringBankSimulatorMock)
        {
            var paymentContext = PaymentContextMocker.GetPaymentContext();

            var sut = new PaymentService(loggerMock.Object,
                processPaymentRequestValidatorMock.Object,
                acquiringBankSimulatorMock.Object,
                paymentContext);

            //there is no payment on the db with this id
            Payment payment = await sut.GetPaymentAsync("00000000-ee44-4006-8bb6-52b7d5a00000");

            Assert.Equal(default, payment);
        }

        [Theory, AutoMoqData]
        public async Task ProcessPaymentAsync_Should_Return_Succesful_Payment(
            Mock<ILogger<PaymentService>> loggerMock,
            Mock<IProcessPaymentRequestValidator> processPaymentRequestValidatorMock,
            Mock<IAcquiringBankSimulator> acquiringBankSimulatorMock)
        {
            var paymentContext = PaymentContextMocker.GetPaymentContext();

            var sut = new PaymentService(loggerMock.Object,
                processPaymentRequestValidatorMock.Object,
                acquiringBankSimulatorMock.Object,
                paymentContext);

            var processPaymentRequest = new ProcessPaymentRequest
            {
                Card = new Card
                {
                    CardNumber = "4543474002249996",
                    Cvv = "956",
                    ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                    ExpirationYear = DateTime.Now.AddMonths(1).Year
                },
                Amount = 255,
                Currency = "EUR",
            };

            processPaymentRequestValidatorMock.Setup(c => c.IsValid(processPaymentRequest)).Returns(true);

            acquiringBankSimulatorMock.Setup(a => a.ProcessPayment(processPaymentRequest)).Returns(new Payment
            {
                ProcessId = Guid.NewGuid(),
                CardNumber = CardHelper.GetMaskedCardNumber(processPaymentRequest.Card.CardNumber),
                Amount = processPaymentRequest.Amount,
                Currency = processPaymentRequest.Currency,
                ResponseCode = ResponseCodes.Approved.Code,
                ResponseSummary = ResponseCodes.Approved.Message,
                Status = "Successful",
                ProcessedAt = DateTime.Now,
            });

            var payment = await sut.ProcessPaymentAsync(processPaymentRequest);

            var paymentFromDb = await sut.GetPaymentAsync(payment.ProcessId.ToString());

            Assert.Equal("Successful", payment.Status);
            Assert.Equal(payment, paymentFromDb);
        }

        [Theory, AutoMoqData]
        public async Task ProcessPaymentAsync_Should_Return_Unsuccesful_Payment(
            Mock<ILogger<PaymentService>> loggerMock,
            Mock<IProcessPaymentRequestValidator> processPaymentRequestValidatorMock,
            Mock<IAcquiringBankSimulator> acquiringBankSimulatorMock)
        {
            var paymentContext = PaymentContextMocker.GetPaymentContext();

            var sut = new PaymentService(loggerMock.Object,
                processPaymentRequestValidatorMock.Object,
                acquiringBankSimulatorMock.Object,
                paymentContext);

            var processPaymentRequest = new ProcessPaymentRequest
            {
                Card = new Card
                {
                    CardNumber = "456464",
                    Cvv = "1",
                    ExpirationMonth = DateTime.Now.AddMonths(1).Month,
                    ExpirationYear = DateTime.Now.AddMonths(1).Year
                },
                Amount = 255,
                Currency = "EUR",
            };

            processPaymentRequestValidatorMock.Setup(c => c.ValidateCard(processPaymentRequest.Card)).Returns(ResponseCodes.SoftDecline.InvalidCardNumber);

            var payment = await sut.ProcessPaymentAsync(processPaymentRequest);

            Assert.Equal("Unsuccessful", payment.Status);
            Assert.Null(payment.ProcessId);
        }
    }
}