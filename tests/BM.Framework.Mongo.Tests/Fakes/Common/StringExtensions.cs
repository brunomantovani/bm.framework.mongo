using System.Linq;

namespace BM.Framework.Mongo.Tests.Fakes.Common
{
    public static class StringExtensions
    {
        public static string ToNumeric(
            this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.ArgumentException(Resources.IsNullOrWhiteSpace, nameof(value));
            }

            return new string(value.Where(char.IsDigit).ToArray());
        }

        public static string ToAlphaNumeric(
            this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.ArgumentException(Resources.IsNullOrWhiteSpace, nameof(value));
            }

            return new string(value.Where(x => char.IsDigit(x) || char.IsLetter(x)).ToArray());
        }
    }
}
