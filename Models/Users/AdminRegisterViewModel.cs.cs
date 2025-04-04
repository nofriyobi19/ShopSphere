using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models.Users;

public class AdminRegisterViewModel {
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
}