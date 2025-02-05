using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models.Users;
using ShopSphere.Services;

namespace ShopSphere.Controllers;

[Route("account")]
public class AccountController(AccountService service) : Controller {
    private readonly AccountService _service = service;

    [Route("")]
    public async Task<IActionResult> Index(int pageNumber, int pageSize, string sortBy, string sort, string username = "", string name = "", string email = "", string role = "") {
        var users = await _service.GetAllUserAsync(pageNumber, pageSize, sortBy, sort, username, name, email, role);
        return View(users);
    }

    [Route("admin/register", Name = "registerAdmin")]
    public IActionResult RegisterAdmin() {
        return View(new AdminRegisterViewModel());
    }

    [Route("user/register", Name = "registerUser")]
    public IActionResult RegisterUser() {
        return View(new UserRegisterViewModel());
    }

    [Route("update/{id}")]
    public async Task<IActionResult> Update(long id) {
        UserUpdateViewModel model = await _service.GetUserUpdateAsync(id);
        return View(model);
    }

    [Route("admin/register/save", Name = "saveAdminRegister")]
    public async Task<IActionResult> SaveAdmin(AdminRegisterViewModel adminRegisterViewModel) {
        await _service.SaveAdminAsync(adminRegisterViewModel);
        return RedirectToAction("index");
    }

    [Route("user/register/save", Name = "saveUserRegister")]
    public async Task<IActionResult> SaveUser(UserRegisterViewModel userRegisterViewModel) {
        await _service.SaveUserAsync(userRegisterViewModel);
        return RedirectToAction("index");
    }

    [Route("update/save", Name = "save")]
    public async Task<IActionResult> SaveUser(UserUpdateViewModel userUpdateViewModel) {
        await _service.SaveUserAsync(userUpdateViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteUserByIdAsync(id);
        return RedirectToAction("index");
    }
}