namespace Finance_Manager_Icarus.DTOs
{
    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string token { get; set; }
        public string nome { get; set; }
    }
}
