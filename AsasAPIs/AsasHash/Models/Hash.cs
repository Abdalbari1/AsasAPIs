namespace Asas.AsasHash.Models
{
    public class Hash
    {
        // Stores the encrypted hash string.
        public string HashedPassword { get; set; } = default!;

        // Stores the plain-text password provided by the user. 
        // Should only be populated temporarily during the request lifecycle.
        public string RawPassword { get; set; } = default!;

        // Indicates the success status of the hashing or verification process.
        public bool IsSucceeded { get; set; } = false;

        public GoodHash GoodHash { get; set; } = default!;
    }
}