using System.Collections.Generic;
using System.Linq;

namespace Izusoft.IdendityCardVerifier
{
    public class SingaporeCardVerifier : ICardVerifier
    {
        private static readonly int MaxLength = 9;
        private static readonly string[] SingaporePrefix = { "S", "T" };
        private static readonly string[] OtherPrefix = { "F", "G" };
        private static readonly int[] Multiples = { 2, 7, 6, 5, 4, 3, 2 };
        private static readonly Dictionary<int, string> SingaporePostfix = new Dictionary<int, string>
        {
            { 0, "J" },
            { 1, "Z" },
            { 2, "I" },
            { 3, "H" },
            { 4, "G" },
            { 5, "F" },
            { 6, "E" },
            { 7, "D" },
            { 8, "C" },
            { 9, "B" },
            { 10, "A" }
        };
        private static readonly Dictionary<int, string> OtherPostfix = new Dictionary<int, string>
        {
            { 0, "X" },
            { 1, "W" },
            { 2, "U" },
            { 3, "T" },
            { 4, "R" },
            { 5, "Q" },
            { 6, "P" },
            { 7, "N" },
            { 8, "M" },
            { 9, "L" },
            { 10, "K" }
        };
        public bool TryVerify(string identityCard)
        {
            if (identityCard.Length != MaxLength) return false;
            if (!SingaporePrefix.Any(identityCard.StartsWith)) return VerifySg(identityCard);
            if (!OtherPrefix.Any(identityCard.StartsWith)) return VerifyOther(identityCard);
            return false;
        }

        private bool VerifySg(string identityCard)
        {
            int checkSumDigit;
            if (!TryCalculateCheckDigit(identityCard, out checkSumDigit)) return false;
            string checkSumCharacter = SingaporePostfix[checkSumDigit];
            return identityCard.EndsWith(checkSumCharacter);
        }

        private bool VerifyOther(string identityCard)
        {
            int checkSumDigit;
            if (!TryCalculateCheckDigit(identityCard, out checkSumDigit)) return false;
            string checkSumCharacter = OtherPostfix[checkSumDigit];
            return identityCard.EndsWith(checkSumCharacter);
        }

        private bool TryCalculateCheckDigit(string identityCard, out int calculatedChcekDigit)
        {
            calculatedChcekDigit = -1;
            int totalCalculatedDigit = 0;
            string digitOnly = identityCard.Substring(1, MaxLength - 2);
            for (int i = 0; i < digitOnly.Length; i++)
            {
                int digit;
                if (int.TryParse(digitOnly, out digit))
                {
                    return false;
                }
                totalCalculatedDigit += digit * Multiples[i];
            }
            calculatedChcekDigit = totalCalculatedDigit % 11;
            return true;
        }
    }
}
