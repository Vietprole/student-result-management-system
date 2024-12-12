using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Services
{
    public class PashwordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hashedPassword = HashPasswordWithArgon2(password, salt);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hashedPassword)}";
        }

        public bool VerifyPassword(string password, string hashedPasswordWithSalt)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid hashed password format.");

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);
            byte[] newHash = HashPasswordWithArgon2(password, salt);

            return CryptographicEquals(storedHash, newHash);
        }

        private byte[] GenerateSalt()
        {
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private byte[] HashPasswordWithArgon2(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                return argon2.GetBytes(32);
            }
        }

        private bool CryptographicEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            bool areEqual = true;
            for (int i = 0; i < a.Length; i++)
            {
                areEqual &= (a[i] == b[i]);
            }

            return areEqual;
        }
    }
}