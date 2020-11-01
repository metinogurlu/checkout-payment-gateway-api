using System.Linq;

namespace PaymentGateway.API.Helpers
{
    public static class CardHelper
    {
        /// <summary>
        /// Hide card number except last four character
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string GetMaskedCardNumber(string cardNumber)
        {
            string firstPartOfCardNumber = cardNumber[0..^4];
            return cardNumber.Replace(firstPartOfCardNumber, string.Concat(Enumerable.Repeat("*", firstPartOfCardNumber.Length)));
        }
    }
}