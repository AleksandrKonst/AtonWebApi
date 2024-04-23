namespace Application.DTO;

public class NewUserDataDto
{
    public string Login { get; set; }
    
    public string Name { get; set; }
    
    public int Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
}