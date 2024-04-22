namespace Application.DTO;

public class UserByLoginDto
{
    public string Name { get; set; }
    
    public int Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    public bool Status { get; set; }
}