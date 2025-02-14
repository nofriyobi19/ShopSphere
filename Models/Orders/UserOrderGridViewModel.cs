using ShopSphere.Models.Home;

namespace ShopSphere.Models.Orders;

public class UserOrderGridViewModel {
    public required GridViewModel<OrderViewModel> Payload { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public required UserNavViewModel Navigation { get; set; }
}