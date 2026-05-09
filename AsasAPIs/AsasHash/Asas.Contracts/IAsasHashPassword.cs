using Asas.AsasHash.Asas.Models;

namespace Asas.AsasHash.Asas.Contracts
{
    public interface IAsasHashPassword
    {
        // Generates a secure hash from the provided raw password context.
        Hash HashPassword(Hash context);

        // Verifies a raw password against a stored hashed password.
        Hash VerifyPassword(Hash context);
    }
}