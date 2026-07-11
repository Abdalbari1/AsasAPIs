namespace AsasAPIs.Models
{
    //This model is specific to JWT Authentication and is used to capture the login response data from the server.
    public class LoginRes
    {
        public string EmpId { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public int ExpiresInSeconds { get; set; }
    }
}
