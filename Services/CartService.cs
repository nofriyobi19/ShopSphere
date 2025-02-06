using ShopSphere.Data;
using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Cart;
using ShopSphere.Models.Orders;

namespace ShopSphere.Services;

public class CartService(ICartRepository cartRepository, IUserRepository userRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ShopSphereContext shopSphereContext) {
    private readonly ICartRepository _cartRepository = cartRepository;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IOrderRepository _orderRepository = orderRepository;

    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;

    private readonly ShopSphereContext dbContext = shopSphereContext;


    public async Task<GridViewModel<CartViewModel>> GetAllUserCartItem(int pageNumber, int pageSize, string sortBy, string sort, string username) {
        sortBy = sortBy is null || sortBy == "" || sortBy.Equals("entry", StringComparison.CurrentCultureIgnoreCase) ? "UserId" : sortBy;
        PaginationViewModel pagination = new(pageNumber, pageSize, sortBy, sort);
        var cartList = await _cartRepository.FindAllAsync(username, pagination);
        return cartList.ToCartViewModel();
    }

    public async Task<CartUpsertViewModel> GetCartUpsertAsync(long cartId) {
        var cart = await _cartRepository.FindByIdAsync(cartId);
        return cart.ToCartUpsertViewModel();
    }

    public async Task<CartViewModel> SaveCartAsync(string username, CartUpsertViewModel cartUpsertViewModel) {
        var user = await _userRepository.FindByUsernameAsync(username);
        Cart cartModel;
        if (cartUpsertViewModel.Id == 0) {
            var cartByProduct = await _cartRepository.FindByProductId(cartUpsertViewModel.ProductId);
            if (cartByProduct != null) {
                cartModel = cartByProduct;
                cartModel.Quantity += cartUpsertViewModel.Quantity;
            } else cartModel = cartUpsertViewModel.ToCart(user.UserId);
        } else cartModel = cartUpsertViewModel.ToCart(user.UserId);
        cartModel = await _cartRepository.SaveAsync(cartModel);
        var cart = await _cartRepository.FindByIdAsync(cartModel.CartId);
        return cart.ToCartViewModel();
    }

    public async Task<CartViewModel> DeleteCartByIdAsync(long cartId) {
        var cart = await _cartRepository.FindByIdAsync(cartId);
        await _cartRepository.DeleteAsync(cart);
        return cart.ToCartViewModel();
    }

    public async Task<List<CartViewModel>> CheckoutAllCartAsync(string username) {
        var user = await _userRepository.FindByUsernameAsync(username);
        var cartList = await _cartRepository.FindAllAsync(user.UserId);

        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try {
            Order newOrder = new() {
                UserId = user.UserId,
                TotalPrice = cartList.Sum(e => e.Product.Price),
                Status = "On Process",
                OrderDate = DateTime.Now
            };

            var order = await _orderRepository.SaveAsync(newOrder);

            foreach (var cart in cartList) {
                await _orderItemRepository.SaveAsync(cart.ToOrderItem(order.OrderId));
                await _cartRepository.DeleteAsync(cart);
            }

            await transaction.CommitAsync();
        } catch {
            await transaction.RollbackAsync();
        }

        return [.. cartList.Select(e => e.ToCartViewModel())];
    }
}