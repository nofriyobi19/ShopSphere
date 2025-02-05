namespace ShopSphere.Models.Users;

public class AdminUpdateViewModel {
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
}