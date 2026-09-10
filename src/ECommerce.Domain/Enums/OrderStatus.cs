namespace ECommerce.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,
    Paid = 1,
    Preparing = 2,
    Shipped = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Cancelled = 6
}