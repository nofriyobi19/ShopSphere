using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models.Users;

public class UserUpdateViewModel {
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
}