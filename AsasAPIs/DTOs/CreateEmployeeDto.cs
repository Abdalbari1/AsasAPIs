namespace AsasAPIs.DTOs
{
    public class CreateEmployeeDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; } 
        public string? Major { get; set; }
        public int ComId { get; set; }
        public int EmpAutoId { get; set; }
    }
}
