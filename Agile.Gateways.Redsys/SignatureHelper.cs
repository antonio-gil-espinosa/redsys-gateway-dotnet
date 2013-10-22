using System;
using System.Security.Cryptography;
using System.Text;
using Agile.Gateways.Redsys.Domain.Model;

namespace Agile.Gateways.Redsys
{
    /// <summary>
    /// Help class for generatig SHA1 signatures.
    /// </summary>
    public static class SignatureHelper
    {
        /// <summary>
        /// The _hash algorithm
        /// </summary>
        private static readonly HashAlgorithm _hashAlgorithm = new SHA1CryptoServiceProvider();

        /// <summary>
        /// Hashes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        private static string Hash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            bytes = _hashAlgorithm.ComputeHash(bytes);

            StringBuilder s = new StringBuilder();

            foreach (byte b in bytes)
                s.Append(b.ToString("x2")
                          .ToLower());

            return s.ToString();
        }

        /// <summary>
        /// Gets a SHA1 signature.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="order">The order.</param>
        /// <param name="merchantCode">The merchant code.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="sum">The sum.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="secret">The secret.</param>
        /// <returns>System.String.</returns>
        public static string GetSignature(decimal amount,
                                          string order,
                                          int merchantCode,
                                          int currency,
                                          decimal sum,
                                          RedsysTransactionType transactionType,
                                          string callback,
                                          string secret)
        {
            return
                Hash(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                                   (int)Math.Round(amount * 100, 2),
                                   order,
                                   merchantCode,
                                   currency,
                                   (int)Math.Round(100 * sum, 2),
                                   (char)transactionType,
                                   callback,
                                   secret));
        }

        /// <summary>
        /// Gets a SHA1 signature.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="order">The order.</param>
        /// <param name="merchantCode">The merchant code.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="secret">The secret.</param>
        /// <returns>System.String.</returns>
        public static string GetSignature(decimal amount,
                                          string order,
                                          int merchantCode,
                                          int currency,
                                          RedsysTransactionType transactionType,
                                          string callback,
                                          string secret)
        {
            string input = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                          (int)Math.Round(amount * 100, 2),
                                          order,
                                          merchantCode,
                                          currency,
                                          (char)transactionType,
                                          callback,
                                          secret);

            return Hash(input);
        }

        /// <summary>
        /// Gets a SHA1 signature.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="order">The order.</param>
        /// <param name="merchantCode">The merchant code.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="secret">The secret.</param>
        /// <returns>System.String.</returns>
        public static string GetSignature(decimal amount, string order, int merchantCode, int currency, string secret)
        {
            return Hash(string.Format("{0}{1}{2}{3}{4}", (int)Math.Round(amount * 100, 2), order, merchantCode, currency, secret));
        }

        /// <summary>
        /// Gets a SHA1 signature.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="order">The order.</param>
        /// <param name="merchantCode">The merchant code.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="sum">The sum.</param>
        /// <param name="secret">The secret.</param>
        /// <returns>System.String.</returns>
        public static string GetSignature(decimal amount, string order, int merchantCode, int currency, decimal sum, string secret)
        {
            return Hash(string.Format("{0}{1}{2}{3}{4}{5}", (int)Math.Round(amount * 100, 2), order, merchantCode, currency, (int)Math.Round(100 * sum,2), secret));
        }

        /// <summary>
        /// Determines whether notification is valid.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="order">The order.</param>
        /// <param name="merchantCode">The merchant code.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="response">The response.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if [notification is valid]; otherwise, <c>false</c>.</returns>
        public static bool IsNotificationValid(decimal amount,
                                               string order,
                                               string merchantCode,
                                               int currency,
                                               int response,
                                               string secret,
                                               string signature)
        {
            int intAmount = (int)Math.Round(amount * 100, 2);
            string input = intAmount + order + merchantCode + currency + response + secret;
            string hash = Hash(input);
            return string.Equals(hash, signature, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}