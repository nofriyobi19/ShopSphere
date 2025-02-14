using Microsoft.AspNetCore.Mvc.Rendering;
using ShopSphere.Models.Home;

namespace ShopSphere.Models.Cart;

public class CartGridViewModel {
    public required GridViewModel<CartViewModel> Payload { get; set; }

    public required UserNavViewModel Navigation { get; set; }
}