namespace AsasAPIs.Models
{
    //This model is specific to JWT Authentication and is used to capture the login request data from the client.
    public class LoginReq
    {
        public string EmpId { get; set; }
        public string Password { get; set; }
    }
}
