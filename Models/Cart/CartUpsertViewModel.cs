namespace ShopSphere.Models.Cart;

public class CartUpsertViewModel {
    public long Id { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public CartUpsertViewModel() {

    }

    public CartUpsertViewModel(long productId) {
        ProductId = productId;
    }
}