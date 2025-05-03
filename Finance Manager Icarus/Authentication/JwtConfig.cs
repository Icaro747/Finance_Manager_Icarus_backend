namespace Finance_Manager_Icarus.Authentication;

public class JwtConfig
{
    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public int ExpiryMinutes { get; set; }
}