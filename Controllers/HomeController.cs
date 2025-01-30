using Microsoft.AspNetCore.Mvc;

namespace ShopSphere.Controllers;

public class HomeController : Controller {
    public IActionResult Index() {
        return RedirectToAction("index", "category");
    }
}