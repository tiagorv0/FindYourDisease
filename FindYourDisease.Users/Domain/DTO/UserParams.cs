namespace FindYourDisease.Users.Domain.DTO;

public class UserParams
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string OrderBy { get; set; }
    public bool Active { get; set; }
    public string Search { get; set; }
}
