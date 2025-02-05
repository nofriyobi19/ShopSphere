namespace ShopSphere.Models.Users;

public class LoginViewModel {
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ReturnUrl { get; set; } = null!;

    public LoginViewModel() {
    }

    public LoginViewModel(string returnUrl) {
        ReturnUrl = returnUrl;
    }
}