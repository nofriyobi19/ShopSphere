using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Users;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("account")]
public class AccountController(AccountService service) : Controller {
    private readonly AccountService _service = service;

    [Route("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, string username = "", string name = "", string email = "", string role = "") {
        var users = await _service.GetAllUserAsync(pageNumber, pageSize, sortBy, sort, username, name, email, role);
        return View(users);
    }

    [Authorize(Roles = "Admin")]
    [Route("admin/register", Name = "registerAdmin")]
    public IActionResult RegisterAdmin() {
        return View(new AdminRegisterViewModel());
    }

    [Route("user/register", Name = "registerUser")]
    public IActionResult RegisterUser() {
        return View(new UserRegisterViewModel());
    }

    [Route("update/{id}")]
    [Authorize]
    public async Task<IActionResult> Update(long id) {
        UserUpdateViewModel model = await _service.GetUserUpdateAsync(id);
        return View(model);
    }

    [Route("admin/register/save", Name = "saveAdminRegister")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SaveAdmin(AdminRegisterViewModel adminRegisterViewModel) {
        await _service.SaveAdminAsync(adminRegisterViewModel);
        return RedirectToAction("index");
    }

    [Route("user/register/save", Name = "saveUserRegister")]
    public async Task<IActionResult> SaveUser(UserRegisterViewModel userRegisterViewModel) {
        await _service.SaveUserAsync(userRegisterViewModel);
        return RedirectToAction("login");
    }

    [Route("update/save", Name = "save")]
    [Authorize]
    public async Task<IActionResult> SaveUser(UserUpdateViewModel userUpdateViewModel) {
        await _service.SaveUserAsync(userUpdateViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteUserByIdAsync(id);
        return RedirectToAction("index");
    }

    [Route("login")]
    public IActionResult Login(string returnUrl = "") {
        return View(new LoginViewModel(returnUrl));
    }

    [Route("login/verify", Name = "verifyLogin")]
    public async Task<IActionResult> VerifyLogin(LoginViewModel loginViewModel) {
        var user = await _service.VerifyUser(loginViewModel);

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role)
        ];

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        if (loginViewModel.ReturnUrl != null) return LocalRedirect(loginViewModel.ReturnUrl);

        return RedirectToAction("index", "home");
    }

    [Route("logout")]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync();
        return RedirectToAction("index", "home");
    }
}