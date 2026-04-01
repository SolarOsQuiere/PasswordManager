using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Services;

public sealed class DeterministicSecretService
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{}<>?";
    private const string PasswordAlphabet = Lowercase + Uppercase + Digits + Symbols;

    public string GeneratePassword(string privateKey, string publicKey, int requestedLength)
    {
        var length = Math.Max(15, requestedLength);
        var bytes = ExpandBytes($"pwd|{privateKey}|{publicKey}|{length}", length * 4);
        var password = new char[length];

        for (var index = 0; index < length; index++)
        {
            password[index] = PasswordAlphabet[bytes[index] % PasswordAlphabet.Length];
        }

        EnforceFirstFifteenRules(password, bytes);
        return new string(password);
    }

    public string GenerateNumericCode(string privateKey, string publicKey, int digits)
    {
        var length = Math.Max(1, digits);
        var bytes = ExpandBytes($"pin|{privateKey}|{publicKey}|{length}", length * 2);
        var result = new char[length];

        for (var index = 0; index < length; index++)
        {
            result[index] = Digits[bytes[index] % Digits.Length];
        }

        return new string(result);
    }

    private static void EnforceFirstFifteenRules(char[] password, byte[] bytes)
    {
        var requiredSets = new[]
        {
            Lowercase,
            Uppercase,
            Digits,
            Symbols
        };

        var chosenPositions = new HashSet<int>();

        for (var index = 0; index < requiredSets.Length; index++)
        {
            var position = bytes[40 + index] % 15;

            while (!chosenPositions.Add(position))
            {
                position = (position + 1) % 15;
            }

            var charset = requiredSets[index];
            password[position] = charset[bytes[50 + index] % charset.Length];
        }
    }

    private static byte[] ExpandBytes(string input, int byteCount)
    {
        var buffer = new List<byte>(byteCount);
        var seed = Encoding.UTF8.GetBytes(input);
        var counter = 0;

        while (buffer.Count < byteCount)
        {
            var counterBytes = BitConverter.GetBytes(counter++);
            var combined = new byte[seed.Length + counterBytes.Length];

            Buffer.BlockCopy(seed, 0, combined, 0, seed.Length);
            Buffer.BlockCopy(counterBytes, 0, combined, seed.Length, counterBytes.Length);

            buffer.AddRange(SHA512.HashData(combined));
        }

        return buffer.Take(byteCount).ToArray();
    }
}
