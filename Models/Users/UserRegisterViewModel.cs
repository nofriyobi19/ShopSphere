using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models.Users;

public class UserRegisterViewModel {
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
}