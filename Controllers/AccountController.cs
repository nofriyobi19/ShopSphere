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

    [Route("admin/update/{id}", Name = "updateAdmin")]
    public async Task<IActionResult> UpdateAdmin(long id) {
        AdminUpdateViewModel model = await _service.GetAdminUpdateAsync(id);
        return View(model);
    }

    [Route("admin/register/save", Name = "saveAdminRegister")]
    public async Task<IActionResult> SaveAdmin(AdminRegisterViewModel adminRegisterViewModel) {
        await _service.SaveAdminAsync(adminRegisterViewModel);
        return RedirectToAction("index");
    }

    [Route("admin/update/save", Name = "saveAdminUpdate")]
    public async Task<IActionResult> SaveAdmin(AdminUpdateViewModel adminUpdateViewModel) {
        await _service.SaveAdminAsync(adminUpdateViewModel);
        return RedirectToAction("index");
    }

    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        await _service.DeleteUserByIdAsync(id);
        return RedirectToAction("index");
    }
}