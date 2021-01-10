using System;
using System.Security.Cryptography;
using System.Text;

namespace DivisorPrimo.Infra.CrossCutting.Util.Extensions
{
    public static class StringExtensions
    {
        public static Guid GetGuid(this string input)
        {
            return new Guid(NumeroUtils.GetHash(input));
        }

        public static string GetHash(this string input)
        {
            return NumeroUtils.GetHash(input);
        }
    }
}
