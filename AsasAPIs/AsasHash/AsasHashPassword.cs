using System;
using Asas.AsasHash.Asas.Contracts;
using BCrypt.Net;
using Asas.AsasHash.Asas.Models;

namespace Asas.AsasHash
{
    public class AsasHashPassword : IAsasHashPassword
    {
        public Hash HashPassword(Hash context)
        {
            if (string.IsNullOrEmpty(context.RawPassword))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(context.RawPassword));
            }

            string generatedHash = BCrypt.Net.BCrypt.HashPassword(context.RawPassword);

            return new Hash
            {
                HashedPassword = generatedHash,
                IsSucceeded = true
            };
        }

        public Hash VerifyPassword(Hash context)
        {
            // Returns a failed result rather than throwing an exception for invalid authentication attempts.
            if (string.IsNullOrWhiteSpace(context.HashedPassword) || string.IsNullOrWhiteSpace(context.RawPassword))
            {
                return new Hash
                {
                    IsSucceeded = false
                };
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(context.RawPassword, context.HashedPassword);

            return new Hash
            {
                IsSucceeded = isValid
            };
        }
    }
}