using System.Globalization;
using ShopSphere.Configurations;
using ShopSphere.Data.Models;
using ShopSphere.Models;
using ShopSphere.Models.Cart;

namespace ShopSphere.Helpers;

public static class CartMapper {
    public static CartViewModel ToCartViewModel(this Cart cart) {
        return new CartViewModel {
            Id = cart.CartId,
            ProductName = cart.Product.Name,
            Price = string.Format(CultureInfo.GetCultureInfo(CultureConfig.CurrencyCulture), "{0:C}", cart.Product.Price),
            Quantity = cart.Quantity,
            CartUpsert = new CartUpsertViewModel {
                Id = cart.CartId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity
            }
        };
    }

    public static GridViewModel<CartViewModel> ToCartViewModel(this GridViewModel<Cart> cartGrid) {
        var content = cartGrid.Content.Select(e => e.ToCartViewModel()).ToList();
        return new GridViewModel<CartViewModel>(content, cartGrid.Pagination);
    }

    public static Cart ToCart(this CartUpsertViewModel cartUpsertViewModel, long userId) {
        return new Cart {
            CartId = cartUpsertViewModel.Id,
            ProductId = cartUpsertViewModel.ProductId,
            UserId = userId,
            Quantity = cartUpsertViewModel.Quantity
        };
    }

    public static CartUpsertViewModel ToCartUpsertViewModel(this Cart cart) {
        return new CartUpsertViewModel {
            Id = cart.CartId,
            ProductId = cart.ProductId,
            Quantity = cart.Quantity
        };
    }

    public static OrderItem ToOrderItem(this Cart cart, long orderId) {
        return new OrderItem {
            OrderId = orderId,
            ProductId = cart.ProductId,
            Quantity = cart.Quantity,
            Price = cart.Product.Price
        };
    }
}