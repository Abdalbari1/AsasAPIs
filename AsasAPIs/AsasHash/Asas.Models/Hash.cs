using Asas.AsasHash.Asas.Models;
namespace Asas.AsasHash.Asas.Models
{
    public class Hash
    {
        // Stores the encrypted hash string.
        public string HashedPassword { get; set; }

        // Stores the plain-text password provided by the user. 
        // Should only be populated temporarily during the request lifecycle.
        public string RawPassword { get; set; }

        // Indicates the success status of the hashing or verification process.
        public bool IsSucceeded { get; set; } = false;

        public GoodHash GoodHash { get; set; }
    }
}