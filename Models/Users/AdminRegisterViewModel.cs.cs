namespace ShopSphere.Models.Users;

public class AdminRegisterViewModel {
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
}