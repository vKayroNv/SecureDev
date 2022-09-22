﻿using System.Security.Cryptography;
using System.Text;

namespace CardStorageService.Core.Utils
{
    internal static class PasswordUtils
    {
        private const string SecretKey = "YkNSQzMnb1YiSzx9RicuY3UiMyp8KCk6Yko/ezJbR2E";

        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            byte[] buffer = RandomNumberGenerator.GetBytes(16);

            string passwordSalt = Convert.ToBase64String(buffer);
            string passwordHash = GetPasswordHash(password, passwordSalt);

            return (passwordSalt, passwordHash);
        }

        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            return GetPasswordHash(password, passwordSalt) == passwordHash;
        }

        public static string GetPasswordHash(string password, string passwordSalt)
        {
            password = $"{password}~{passwordSalt}~{SecretKey}";

            byte[] passwordHash = SHA512.HashData(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(passwordHash);
        }
    }
}
