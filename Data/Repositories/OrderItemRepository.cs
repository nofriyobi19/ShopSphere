using ShopSphere.Data.Models;
using ShopSphere.Data.Repositories.Interfaces;

namespace ShopSphere.Data.Repositories;

public class OrderItemRepository(ShopSphereContext shopSphereContext) : CrudRepository<OrderItem, long>(shopSphereContext), IOrderItemRepository {

}