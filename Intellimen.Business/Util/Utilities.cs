using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Intellimen.Business.Util
{
    public static class Utilities
    {
        //Codificador
        public static string SHA256(string input) =>
           SHA256(Encoding.UTF8.GetBytes(input));

        public static string SHA256(byte[] input)
        {
            using SHA256 hash =
                System.Security.Cryptography.SHA256.Create();
            return BitConverter.ToString(hash.ComputeHash(input))
            .Replace("-", "").ToLower();
        }

        public static bool TryGetClaim(this ClaimsPrincipal? claims, string? claimId,
           [MaybeNullWhen(false)] out string? value)
        {
            string? valRef = claims.FindFirstValue(claimId);
            if (!string.IsNullOrEmpty(valRef))
            {
                value = valRef;
                return true;
            }

            value = default;
            return false;
        }
    }
}
