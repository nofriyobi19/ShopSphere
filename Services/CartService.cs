using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Models.Cart;

namespace ShopSphere.Services;

public class CartService(ICartRepository cartRepository, IUserRepository userRepository) {
    private readonly ICartRepository _cartRepository = cartRepository;

    private readonly IUserRepository _userRepository = userRepository;

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
}