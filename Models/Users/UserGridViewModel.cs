using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopSphere.Models.Users;

public class UserGridViewModel {
    public required GridViewModel<UserViewModel> Payload { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
    
    public List<SelectListItem> RoleDropdown => [
        new SelectListItem{ Text = "Admin", Value = "Admin" },
        new SelectListItem{ Text = "User", Value = "User" }
    ];
}