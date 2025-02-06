namespace ShopSphere.Models.Orders;

public class UserOrderGridViewModel {
    public required GridViewModel<OrderViewModel> Payload { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}