namespace Domain.Models.Service;

public class UserModel
{
    public bool? IsDeleted { get; set; }
    public string FullName { get; set; } = null!;
    public int PhoneNumber { get; set; }
}