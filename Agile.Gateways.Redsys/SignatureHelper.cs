using System;
using System.Security.Cryptography;
using System.Text;
using Agile.Gateways.Redsys.Domain.Model;

namespace Agile.Gateways.Redsys
{
    public static class SignatureHelper
    {
        private static readonly HashAlgorithm _hashAlgorithm = new SHA1CryptoServiceProvider();

        /// <summary>
        ///     Hashes the specified input.
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

        public static string GetSignature(decimal amount, string order, int merchantCode, int currency, string secret)
        {
            return Hash(string.Format("{0}{1}{2}{3}{4}", (int)Math.Round(amount * 100, 2), order, merchantCode, currency, secret));
        }

        public static string GetSignature(decimal amount, string order, int merchantCode, int currency, decimal sum, string secret)
        {
            return Hash(string.Format("{0}{1}{2}{3}{4}{5}", (int)Math.Round(amount * 100, 2), order, merchantCode, currency, (int)Math.Round(100 * sum,2), secret));
        }

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