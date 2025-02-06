namespace ShopSphere.Models.Orders;

public class OrderGridViewModel {
    public required GridViewModel<OrderViewModel> Payload { get; set; }

    public string BuyerName { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}